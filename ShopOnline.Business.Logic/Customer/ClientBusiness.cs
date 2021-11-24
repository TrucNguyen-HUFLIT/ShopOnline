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
            var currentBrandIds = BrandSingleton.Instance.BrandInfors.Select(x => x.Id).ToList();

            var productsDetailInfor = await _context.ProductDetails
                                        .Where(x => currentBrandIds.Contains(x.ProductType.IdBrand)
                                                && !x.IsDelete && !x.ProductType.IsDelete)
                                        .Select(x => new
                                        {
                                            x.Id,
                                            x.Name,
                                            x.Price,
                                            x.Pic1,
                                            BrandId = x.ProductType.IdBrand
                                        })
                                        .OrderByDescending(x => x.Id)
                                        .ToListAsync();

            foreach (var brandId in currentBrandIds)
            {
                productsInfor.Add(new ProductInforViewModel
                {
                    BrandInfor = BrandSingleton.Instance.BrandInfors.Where(x => x.Id == brandId).FirstOrDefault(),
                    ProductInforsDetail = productsDetailInfor.Where(x => x.BrandId == brandId)
                                        .Select(x => new ProductInforModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Price = x.Price,
                                            Pic = x.Pic1
                                        })
                                        .Take(amountTake ?? 0)
                                        .ToList(),
                });
            }

            #region init data to demo
            for (int i = 0; i < productsInfor.Count(); i++)
            {
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[1].ProductInforsDetail[0]));
                productsInfor[i].ProductInforsDetail[0].Price = 10043000;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[0]));
                productsInfor[i].ProductInforsDetail[1].Price = 110900;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[1]));
                productsInfor[i].ProductInforsDetail[2].Price = 123123;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[2]));
                productsInfor[i].ProductInforsDetail[3].Price = 10123100;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[3]));
                productsInfor[i].ProductInforsDetail[4].Price = 5012300;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[1].ProductInforsDetail[0]));
                productsInfor[i].ProductInforsDetail[5].Price = 1000700;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[0]));
                productsInfor[i].ProductInforsDetail[6].Price = 1106300;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[1]));
                productsInfor[i].ProductInforsDetail[7].Price = 123123;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[2]));
                productsInfor[i].ProductInforsDetail[8].Price = 104123100;
                productsInfor[i].ProductInforsDetail.Add(CreateDemoProduct(productsInfor[i].ProductInforsDetail[3]));
                productsInfor[i].ProductInforsDetail[9].Price = 50900;
                productsInfor[i].ProductInforsDetail = productsInfor[i].ProductInforsDetail.Take(amountTake ?? 0).ToList();
            }
            #endregion

            return productsInfor;
        }

        public async Task<ProductDetailViewModel> GetDetailProductAsync(int id)
        {
            var productDetail = await _context.ProductDetails
                                        .Where(x => x.Id == id && x.Status == ProductStatus.Available
                                                && !x.IsDelete && !x.ProductType.IsDelete)
                                        .Select(x => new ProductDetailViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Pic1 = x.Pic1,
                                            Pic2 = x.Pic2,
                                            Pic3 = x.Pic3,
                                            Description = x.Description,
                                            Price = x.Price,
                                            BrandInfor = new BrandInforModel
                                            {
                                                Id = x.ProductType.IdBrand,
                                                Name = x.ProductType.Brand.Name
                                            },
                                            BaseProductInfors = x.Products
                                                            .Where(y => !y.IsDelete)
                                                            .Select(y => new BaseProductInfor
                                                            {
                                                                Id = y.Id,
                                                                Quantity = y.Quantity,
                                                                Size = y.Size
                                                            })
                                                            .ToList(),
                                            ReviewsDetail = x.ReviewDetails
                                                            .Where(y => !y.IsDelete)
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

            var availableSize = productDetail.BaseProductInfors
                                        .Where(x => x.Quantity != 0)
                                        .Select(x => x.Size)
                                        .ToList();
            var productSizes = Enum.GetValues(typeof(ProductSize))
                           .Cast<ProductSize>()
                           .ToList();

            var productSizeInfors = new List<ProductSizeInfor>();

            foreach (var productSize in productSizes)
            {
                productSizeInfors.Add(new ProductSizeInfor
                {
                    Size = productSize,
                    IsAvailable = availableSize.Contains(productSize)
                });
            }
            productDetail.ProductSizeInfors = productSizeInfors;

            return productDetail;
        }

        public async Task<List<ProductInforModel>> GetCurrentProductsInforAsync(int amountTake)
        {
            var products = await _context.ProductDetails
                                         .Where(x => !x.IsDelete)
                                         .Select(x => new ProductInforModel
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             Price = x.Price,
                                             Pic = x.Pic1
                                         })
                                        .Take(amountTake)
                                        .ToListAsync();
            #region init data to demo
            products.Add(CreateDemoProduct(products[0]));
            products[0].Price = 1004000;
            products.Add(CreateDemoProduct(products[1]));
            products[1].Price = 100000;
            products.Add(CreateDemoProduct(products[2]));
            products[2].Price = 108000;
            products.Add(CreateDemoProduct(products[3]));
            products[3].Price = 7345;
            products.Add(CreateDemoProduct(products[4]));
            products[4].Price = 64586923;
            products.Add(CreateDemoProduct(products[5]));
            products[5].Price = 523462;
            products.Add(CreateDemoProduct(products[6]));
            products[6].Price = 523363;
            products.Add(CreateDemoProduct(products[7]));
            products[7].Price = 4575685;
            products.Add(CreateDemoProduct(products[8]));
            products[8].Price = 34521315;
            products.Add(CreateDemoProduct(products[9]));
            products[9].Price = 75467358;
            products = products.Take(amountTake).ToList();
            #endregion

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

        public async Task InitBrands()
        {
            var brandInfors = await _context.Brands
                                .Where(x => !x.IsDelete)
                                .Select(x => new BrandInforModel
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                })
                                .ToListAsync();
            BrandSingleton.Instance.Init(brandInfors);
        }

        public async Task<TypeOfBrandInforModel> GetTypesOfBrandAsync(int brandId)
        {
            var brandInfor = await _context.Brands
                                    .Where(x => x.Id == brandId && !x.IsDelete)
                                    .Select(x => new
                                    {
                                        BrandId = x.Id,
                                        Type = x.ProductTypes,
                                        BrandName = x.Name,
                                    }).FirstOrDefaultAsync();

            var types = new TypeOfBrandInforModel
            {
                BrandInfor = new BrandInforModel
                {
                    Id = brandInfor.BrandId,
                    Name = brandInfor.BrandName
                },
                TypeInfors = brandInfor.Type.Select(x => new TypeInforModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return types;
        }

        public async Task<ProductsViewModel> GetProductsByBrandAsync(int brandId, int? typeId)
        {
            var productsQuery = _context.ProductDetails.Where(x => x.ProductType.IdBrand == brandId
                                                                && !x.IsDelete)
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
                Price = x.Price,
                Pic = x.Pic1
            })
            .ToListAsync();

            #region init data to demo
            if (productsInfor.Count != 0)
            {
                productsInfor.Add(CreateDemoProduct(productsInfor[0]));
                productsInfor[0].Price = 1004000;
                productsInfor.Add(CreateDemoProduct(productsInfor[1]));
                productsInfor[1].Price = 100000;
                productsInfor.Add(CreateDemoProduct(productsInfor[2]));
                productsInfor[2].Price = 108000;
                productsInfor.Add(CreateDemoProduct(productsInfor[3]));
                productsInfor[3].Price = 7345;
                productsInfor.Add(CreateDemoProduct(productsInfor[4]));
                productsInfor[4].Price = 64586923;
                productsInfor.Add(CreateDemoProduct(productsInfor[5]));
                productsInfor[5].Price = 523462;
                productsInfor.Add(CreateDemoProduct(productsInfor[6]));
                productsInfor[6].Price = 523363;
                productsInfor.Add(CreateDemoProduct(productsInfor[7]));
                productsInfor[7].Price = 4575685;
                productsInfor.Add(CreateDemoProduct(productsInfor[8]));
                productsInfor[8].Price = 34521315;
                productsInfor.Add(CreateDemoProduct(productsInfor[9]));
                productsInfor[9].Price = 75467358;
                productsInfor.Add(CreateDemoProduct(productsInfor[10]));
                productsInfor[10].Price = 445467358;
            }
            #endregion

            return new ProductsViewModel 
            {
                AmountProduct = amountProduct,
                ProductsInfor = productsInfor
            };
        }

        #region init data to demo
        private static ProductInforModel CreateDemoProduct(ProductInforModel old)
        {
            return new ProductInforModel
            {
                Id = old.Id,
                Name = old.Name,
                Price = old.Price,
                Pic = old.Pic
            };
        }
        #endregion
    }
}
