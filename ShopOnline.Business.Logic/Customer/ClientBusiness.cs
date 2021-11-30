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

            var productsDetail = await _context.ProductDetails
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

            foreach (var brandId in currentBrandIds)
            {
                productsInfor.Add(new ProductInforViewModel
                {
                    BrandInfor = BrandSingleton.Instance.BrandInfors.Where(x => x.Id == brandId).FirstOrDefault(),
                    ProductsInforDetail = productsInforDetail,
                });
            }

            #region init data to demo
            for (int i = 0; i < productsInfor.Count; i++)
            {
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[1].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[1].PriceVND = 10043000;
                productsInfor[i].ProductsInforDetail[1].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(10043000);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[2].PriceVND = 110900;
                productsInfor[i].ProductsInforDetail[2].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(110900);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[3].PriceVND = 123123;
                productsInfor[i].ProductsInforDetail[3].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(123123);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[4].PriceVND = 10123100;
                productsInfor[i].ProductsInforDetail[4].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(10123100);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[5].PriceVND = 5012300;
                productsInfor[i].ProductsInforDetail[5].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(5012300);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[1].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[6].PriceVND = 1000700;
                productsInfor[i].ProductsInforDetail[6].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(1000700);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[7].PriceVND = 1106300;
                productsInfor[i].ProductsInforDetail[7].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(1106300);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[8].PriceVND = 123123;
                productsInfor[i].ProductsInforDetail[8].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(123123);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[9].PriceVND = 104123100;
                productsInfor[i].ProductsInforDetail[9].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(104123100);
                productsInfor[i].ProductsInforDetail.Add(CreateDemoProduct(productsInfor[i].ProductsInforDetail[0]));
                productsInfor[i].ProductsInforDetail[10].PriceVND = 50900;
                productsInfor[i].ProductsInforDetail[10].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(50900);

                productsInfor[i].ProductsInforDetail = productsInfor[i].ProductsInforDetail.Take(amountTake ?? 0).ToList();
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
                                            PriceVND = x.Price,
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
            productDetail.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(productDetail.PriceVND);

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
                                             PriceVND = x.Price,
                                             Pic = x.Pic1
                                         })
                                        .Take(amountTake)
                                        .ToListAsync();

            foreach (var product in products)
            {
                product.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(product.PriceVND);
            }

            #region init data to demo
            products.Add(CreateDemoProduct(products[0]));
            products[1].PriceVND = 1004000;
            products[1].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(1004000);
            products.Add(CreateDemoProduct(products[1]));
            products[2].PriceVND = 100000;
            products[2].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(100000);
            products.Add(CreateDemoProduct(products[2]));
            products[3].PriceVND = 108000;
            products[3].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(108000);
            products.Add(CreateDemoProduct(products[3]));
            products[4].PriceVND = 7345;
            products[4].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(7345);
            products.Add(CreateDemoProduct(products[4]));
            products[5].PriceVND = 64586923;
            products[5].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(64586923);
            products.Add(CreateDemoProduct(products[5]));
            products[6].PriceVND = 523462;
            products[6].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(523462);
            products.Add(CreateDemoProduct(products[6]));
            products[7].PriceVND = 523363;
            products[7].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(523363);
            products.Add(CreateDemoProduct(products[7]));
            products[8].PriceVND = 4575685;
            products[8].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(4575685);
            products.Add(CreateDemoProduct(products[8]));
            products[9].PriceVND = 34521315;
            products[9].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(34521315);
            products.Add(CreateDemoProduct(products[9]));
            products[10].PriceVND = 75467358;
            products[10].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(75467358);
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
                PriceVND = x.Price,
                Pic = x.Pic1
            })
            .ToListAsync();

            foreach (var product in productsInfor)
            {
                product.PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(product.PriceVND);
            }

            #region init data to demo
            if (productsInfor.Count != 0)
            {
                productsInfor.Add(CreateDemoProduct(productsInfor[0]));
                productsInfor[1].PriceVND = 1004000;
                productsInfor[1].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(1004000);
                productsInfor.Add(CreateDemoProduct(productsInfor[1]));
                productsInfor[2].PriceVND = 100000;
                productsInfor[2].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(100000);
                productsInfor.Add(CreateDemoProduct(productsInfor[2]));
                productsInfor[3].PriceVND = 108000;
                productsInfor[3].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(108000);
                productsInfor.Add(CreateDemoProduct(productsInfor[3]));
                productsInfor[4].PriceVND = 7345;
                productsInfor[4].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(7345);
                productsInfor.Add(CreateDemoProduct(productsInfor[4]));
                productsInfor[5].PriceVND = 64586923;
                productsInfor[5].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(64586923);
                productsInfor.Add(CreateDemoProduct(productsInfor[5]));
                productsInfor[6].PriceVND = 523462;
                productsInfor[6].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(523462);
                productsInfor.Add(CreateDemoProduct(productsInfor[6]));
                productsInfor[7].PriceVND = 523363;
                productsInfor[7].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(523363);
                productsInfor.Add(CreateDemoProduct(productsInfor[7]));
                productsInfor[8].PriceVND = 4575685;
                productsInfor[8].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(4575685);
                productsInfor.Add(CreateDemoProduct(productsInfor[8]));
                productsInfor[9].PriceVND = 34521315;
                productsInfor[9].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(34521315);
                productsInfor.Add(CreateDemoProduct(productsInfor[9]));
                productsInfor[10].PriceVND = 75467358;
                productsInfor[10].PriceUSD = await ConvertCurrencyHelper.ConvertVNDToUSD(75467358);
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
                PriceVND = old.PriceVND,
                PriceUSD = old.PriceUSD,
                Pic = old.Pic
            };
        }
        #endregion
    }
}
