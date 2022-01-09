using Microsoft.EntityFrameworkCore;
using ShopOnline.Business.Customer;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Helpers;
using ShopOnline.Core.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Business.Logic.Customer
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly MyDbContext _context;

        public ClientBusiness(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductInforViewModel>> GetProductsAsync(int? amountTake)
        {
            var productsInfor = new List<ProductInforViewModel>();
            var currentTypeIds = ProductTypeSingleton.Instance.ProductTypeInfors.Select(x => x.Id).ToList();

            var productsDetail = await _context.ProductDetails
                                        .Where(x => currentTypeIds.Contains(x.IdProductType)
                                                && !x.IsDeleted && !x.ProductType.IsDeleted)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            x.Name,
                                            x.Price,
                                            x.Pic1
                                        })
                                        .OrderByDescending(x => x.Id)
                                        .Take(amountTake ?? 0)
                                        .ToListAsync();
            var productsInforDetail = new List<ProductInforModel>();

            foreach (var product in productsDetail)
            {
                var priceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(product.Price);
                productsInforDetail.Add(new ProductInforModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    PriceVND = product.Price,
                    PriceUSD = priceUSD,
                    Pic = product.Pic1
                });
            }

            foreach (var typeId in currentTypeIds)
            {
                productsInfor.Add(new ProductInforViewModel
                {
                    TypeInfor = ProductTypeSingleton.Instance.ProductTypeInfors.Where(x => x.Id == typeId).FirstOrDefault(),
                    ProductsInforDetail = productsInforDetail,
                });
            }

            return productsInfor;
        }

        public async Task<ProductDetailViewModel> GetDetailProductAsync(int id)
        {
            var productDetail = await _context.ProductDetails
                                        .Where(x => x.Id == id && x.Status == ProductStatus.Available
                                                && !x.IsDeleted && !x.ProductType.IsDeleted)
                                        .Select(x => new ProductDetailViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Pic1 = x.Pic1,
                                            Pic2 = x.Pic2,
                                            Pic3 = x.Pic3,
                                            Description = x.Description,
                                            PriceVND = x.Price,
                                            TypeInfor = new TypeInforModel
                                            {
                                                Id = x.IdProductType,
                                                Name = x.ProductType.Name
                                            },
                                            Quantity = x.Quantity,
                                            ReviewsDetail = x.ReviewDetails
                                                            .Where(y => !y.IsDeleted && y.ReviewStatus == ReviewStatus.Approved)
                                                            .Select(y => new ReviewDetailViewModel
                                                            {
                                                                Id = y.Id,
                                                                IdProductDetail = x.Id,
                                                                Content = y.Content,
                                                                ReviewTime = y.ReviewTime,
                                                                IdCustomer = y.IdCustomer,
                                                                CustomerFullName = y.Customer.FullName
                                                            })
                                                            .OrderByDescending(y => y.ReviewTime)
                                                            .ToList()
                                        })
                                        .SingleOrDefaultAsync();
            productDetail.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(productDetail.PriceVND);

            return productDetail;
        }

        public async Task<List<ProductInforModel>> GetCurrentProductsInforAsync(int amountTake)
        {
            var products = await _context.ProductDetails
                                         .Where(x => !x.IsDeleted)
                                         .Select(x => new ProductInforModel
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             PriceVND = x.Price,
                                             Pic = x.Pic1
                                         })
                                        .Take(amountTake)
                                        .ToListAsync();

            foreach (var product in products)
            {
                product.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(product.PriceVND);
            }

            return products;
        }

        public async Task CreateReviewDetailAsync(ReviewDetailModel reviewDetail)
        {
            var reviewDetailEntity = new ReviewDetailEntity
            {
                Content = reviewDetail.Content,
                ReviewTime = DateTime.Now,
                IdCustomer = reviewDetail.IdCustomer,
                IdProductDetail = reviewDetail.IdProductDetail
            };

            _context.ReviewDetails.Add(reviewDetailEntity);
            await _context.SaveChangesAsync();
        }

        public async Task InitTypes()
        {
            var productTypesInfor = await _context.ProductTypes
                                .Where(x => !x.IsDeleted)
                                .Select(x => new TypeInforModel
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                })
                                .ToListAsync();
            ProductTypeSingleton.Instance.Init(productTypesInfor);
        }

        public async Task<TypeInforModel> GetTypesAsync(int typeId)
        {
            var typesInfor = await _context.ProductTypes
                                    .Where(x => x.Id == typeId && !x.IsDeleted)
                                    .Select(x => new TypeInforModel
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                    }).FirstOrDefaultAsync();
           
            return typesInfor;
        }

        public async Task<ProductsViewModel> GetProductsByTypeAsync(int typeId)
        {
            var productsQuery = _context.ProductDetails.Where(x => x.IdProductType == typeId
                                                                && !x.IsDeleted)
                                        .AsQueryable();
            var amountProduct = await productsQuery.CountAsync();

            if (typeId != null)
            {
                productsQuery = productsQuery.Where(x => x.IdProductType == typeId);
            }

            var productsInfor = await productsQuery.Select(x => new ProductInforModel
            {
                Id = x.Id,
                Name = x.Name,
                PriceVND = x.Price,
                Pic = x.Pic1
            })
            .ToListAsync();

            foreach (var product in productsInfor)
            {
                product.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(product.PriceVND);
            }

            return new ProductsViewModel
            {
                AmountProduct = amountProduct,
                ProductsInfor = productsInfor
            };
        }
    }
}
