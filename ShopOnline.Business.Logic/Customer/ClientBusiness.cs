using Microsoft.EntityFrameworkCore;
using ShopOnline.Business.Customer;
using ShopOnline.Core;
using ShopOnline.Core.Models.Client;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Business.Logic.Customer
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly MyDbContext _context;

        public ClientBusiness(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ProductForHomePageModel> GetProductForHomePageAsync()
        {
            var productTypeInfors = await _context.ProductTypes
                                        .Select(x => new ProductTypeInforModel 
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            BrandName = x.Brand.Name,
                                        })
                                        .ToArrayAsync();

            var productDetailInfors = await _context.ProductDetails
                                        .Select(x=> new ProductDetailInforModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Price = x.Price,
                                            ProductTypeInfor = new ProductTypeInforModel
                                            {
                                                Id = x.IdProductType,
                                                Name = x.ProductType.Name,
                                                BrandName = x.ProductType.Brand.Name
                                            }
                                        })
                                        .ToArrayAsync();

            return new ProductForHomePageModel
            {
                ProductDetailsInfors = productDetailInfors,
                ProductTypeInfors = productTypeInfors
            };
        }
    }
}
