using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopOnline.Business.Customer;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Helpers;
using ShopOnline.Core.Models.Client;
using ShopOnline.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Business.Logic.Customer
{
    public class CartBusiness : ICartBusiness
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _context;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CartBusiness(IHttpContextAccessor httpContextAccessor,
                            MyDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public List<ProductCartModel> GetProductsCart()
        {
            string jsonCart = _session.GetString(CART.CART_KEY);
            if (jsonCart != null)
            {
                return JsonConvert.DeserializeObject<List<ProductCartModel>>(jsonCart);
            }
            return new List<ProductCartModel>();
        }

        public async Task AddProductToCartAsync(int idProduct, int quantity)
        {
            var cart = GetProductsCart();
            var productInCart = cart.Where(x => x.Id == idProduct).FirstOrDefault();

            if (productInCart == null)
            {
                productInCart = await _context.Products.Where(x => x.Id == idProduct && !x.IsDeleted)
                                    .Select(x => new ProductCartModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Quantity = x.Quantity,
                                        PriceVND = x.ProductDetail.Price,
                                        BasePrice = x.ProductDetail.BasePrice,
                                        Pic = x.ProductDetail.Pic1,
                                        Size = x.Size,
                                        IdProductDetail = x.IdProductDetail,
                                    })
                                    .FirstOrDefaultAsync();

                if (productInCart == null)
                    throw new UserFriendlyException(ErrorCode.NotFoundInCart);

                productInCart.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(productInCart.PriceVND);
                cart.Add(productInCart);
            }

            productInCart.SelectedQuantity = quantity == 0
                                    ? productInCart.SelectedQuantity++
                                    : productInCart.SelectedQuantity + quantity;

            if (productInCart.SelectedQuantity > productInCart.Quantity)
                throw new UserFriendlyException(ErrorCode.OutOfStock);

            productInCart.TotalVND = productInCart.PriceVND * productInCart.SelectedQuantity;
            productInCart.TotalUSD = productInCart.PriceUSD * productInCart.SelectedQuantity;
            productInCart.TotalBasePrice = productInCart.BasePrice * productInCart.SelectedQuantity;

            SaveCartSession(cart);
        }

        public Task ReduceProductFromCartAsync(int idProduct, int? quantity)
        {
            var cart = GetProductsCart();
            var productInCart = cart.Where(x => x.Id == idProduct).FirstOrDefault();

            if (productInCart == null)
                throw new UserFriendlyException(ErrorCode.NotFoundInCart);

            productInCart.SelectedQuantity = quantity == null
                                    ? productInCart.SelectedQuantity--
                                    : productInCart.SelectedQuantity - (int)quantity;

            if (productInCart.SelectedQuantity < 0)
                productInCart.SelectedQuantity = 0;

            productInCart.TotalVND = productInCart.PriceVND * productInCart.SelectedQuantity;
            productInCart.TotalUSD = productInCart.PriceUSD * productInCart.SelectedQuantity;
            productInCart.TotalBasePrice = productInCart.BasePrice * productInCart.SelectedQuantity;

            SaveCartSession(cart);

            if (productInCart.SelectedQuantity == 0)
                RemoveProductFromCartAsync(idProduct);

            return Task.CompletedTask;
        }

        public Task RemoveProductFromCartAsync(int idProduct)
        {
            var cart = GetProductsCart();
            var productInCart = cart.Where(x => x.Id == idProduct).FirstOrDefault();

            if (productInCart == null)
                throw new UserFriendlyException(ErrorCode.NotFoundInCart);

            cart.Remove(productInCart);

            SaveCartSession(cart);
            return Task.CompletedTask;
        }

        public Task RemoveAllProductFromCartAsync()
        {
            SaveCartSession(new List<ProductCartModel>());
            return Task.CompletedTask;
        }

        public async Task<int> CheckOutAsync(ClaimsPrincipal user, PaymentMethod paymentMethod, string address)
        {
            string email = user.FindFirst(ClaimTypes.Email).Value;
            string phone = user.FindFirst(ClaimTypes.MobilePhone).Value;
            var orderDetails = new List<OrderDetailEntity>();
            var cart = GetProductsCart();
            if (!cart.Any()) throw new UserFriendlyException(ErrorCode.EmptyCart);

            var productIds = cart.Select(x => x.Id).ToArray();
            var products = await _context.Products.Where(x => !x.IsDeleted && productIds.Contains(x.Id)).ToArrayAsync();
            var idCustomer = await _context.Customers
                                    .Where(x => !x.IsDeleted && x.Email == email && x.PhoneNumber == phone)
                                    .Select(x => x.Id)
                                    .FirstOrDefaultAsync();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                OrderEntity orderEntity = new()
                {
                    OrderDay = DateTime.Now,
                    StatusOrder = StatusOrder.Processing,
                    IdCustomer = idCustomer,
                    Address = address,
                    IsPaid = paymentMethod == PaymentMethod.ShipCod,
                    Payment = paymentMethod,
                    ExtraFee = cart.Sum(x => x.TotalVND) > 5000000 ? 0 : 5000000,
                };
                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();

                foreach (var product in cart)
                {
                    var productEntity = products.Where(x => x.Id == product.Id).FirstOrDefault();
                    int newQuantity = productEntity.Quantity - product.SelectedQuantity;

                    if (newQuantity < 0)
                    {
                        throw new UserFriendlyException(ErrorCode.OutOfStock);
                    }
                    else
                    {
                        productEntity.Quantity = newQuantity;
                    }
                    _context.Products.Update(productEntity);

                    orderDetails.Add(new OrderDetailEntity
                    {
                        IdOrder = orderEntity.Id,
                        IdProduct = product.Id,
                        TotalPrice = product.TotalVND,
                        TotalBasePrice = product.TotalBasePrice,
                        QuantityPurchased = product.SelectedQuantity,
                    });
                }
                _context.OrderDetails.AddRange(orderDetails);

                await _context.SaveChangesAsync();
                await RemoveAllProductFromCartAsync();
                return orderEntity.Id;
            }
        }

        public async Task<OrderInfor> GetOrderById(int id)
        {
            var orderInfor = await _context.Orders
                                        .Where(x => !x.IsDeleted && x.Id == id)
                                        .Select(x => new OrderInfor
                                        {
                                            Id = x.Id,
                                            Addess = x.Address,
                                            FullName = x.Customer.FullName,
                                            PriceVND = x.OrderDetails.Sum(orderDetail => orderDetail.TotalPrice),
                                            Phone = x.Customer.PhoneNumber
                                        })
                                        .FirstOrDefaultAsync();
            return orderInfor;
        }

        private void ClearCart() => _session.Remove(CART.CART_KEY);

        private void SaveCartSession(List<ProductCartModel> products)
        {
            string jsonCart = JsonConvert.SerializeObject(products);
            _session.SetString(CART.CART_KEY, jsonCart);
        }

        private int QuantityProductCart()
        {
            return GetProductsCart().Sum(x => x.SelectedQuantity);
        }
    }
}
