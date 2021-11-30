using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Business.Staff;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Logic.Staff
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductBusiness(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task CreateBrandAsync(BrandCreate brandCreate)
        {
            var brand = await _context.Brands.Where(x => x.Name == brandCreate.BrandName && !x.IsDeleted).Select(x => x.Name).FirstOrDefaultAsync();
            if (brand == null)
            {
                var brandEntity = new BrandEntity
                {
                    Name = brandCreate.BrandName,
                };
                _context.Brands.Add(brandEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BrandExistedException(brandCreate.BrandName);
            }
        }

        public async Task<bool> EditBrandAsync(BrandInfor brandInfor)
        {
            var brandEntity = await _context.Brands.Where(x => x.Id == brandInfor.Id && !x.IsDeleted).FirstOrDefaultAsync();
            if (brandEntity.Name == brandInfor.BrandName)
            {
                throw new BrandExistedException(brandInfor.BrandName);
            }
            brandEntity.Name = brandInfor.BrandName;

            _context.Brands.Update(brandEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public BrandInfor GetBrandByIdAsync(int id)
        {
            var brand = _context.Brands.Where(x => x.Id == id && !x.IsDeleted).Select(x => new BrandInfor
            {
                Id = x.Id,
                BrandName = x.Name,
            }).FirstOrDefault();

            return brand;
        }

        public async Task<IPagedList<BrandInfor>> GetListBrandAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listBrand = new List<BrandInfor>();
            var brands = await _context.Brands.Where(x => !x.IsDeleted).ToListAsync();
            if (brands != null)
            {
                foreach (var brand in brands)
                {
                    var brandInfor = new BrandInfor
                    {
                        Id = brand.Id,
                        BrandName = brand.Name
                    };
                    listBrand.Add(brandInfor);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listBrand = listBrand.Where(s => s.BrandName.Contains(searchString)).ToList();
                }
                listBrand = sortOrder switch
                {
                    "name_desc" => listBrand.OrderByDescending(x => x.BrandName).ToList(),
                    "name" => listBrand.OrderBy(x => x.BrandName).ToList(),
                    "id_desc" => listBrand.OrderByDescending(x => x.Id).ToList(),
                    _ => listBrand.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listBrand.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<IPagedList<ProductTypeInfor>> GetListProductTypeAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listProductType = new List<ProductTypeInfor>();
            var productTypes = await _context.ProductTypes.Where(x => !x.IsDeleted).ToListAsync();
            if (productTypes != null)
            {
                foreach (var product in productTypes)
                {
                    var productType = new ProductTypeInfor
                    {
                        Id = product.Id,
                        Name = product.Name,
                        IdBrand = product.IdBrand
                    };
                    listProductType.Add(productType);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listProductType = listProductType.Where(s => s.Name.Contains(searchString)).ToList();
                }
                listProductType = sortOrder switch
                {
                    "name_desc" => listProductType.OrderByDescending(x => x.Name).ToList(),
                    "name" => listProductType.OrderBy(x => x.Name).ToList(),
                    "id_desc" => listProductType.OrderByDescending(x => x.Id).ToList(),
                    _ => listProductType.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listProductType.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public ProductTypeInfor GetProductTypeByIdAsync(int id)
        {
            var productType = _context.ProductTypes.Where(x => x.Id == id && !x.IsDeleted).Select(x => new ProductTypeInfor
            {
                Id = x.Id,
                Name = x.Name,
                IdBrand = x.IdBrand,
            }).FirstOrDefault();

            return productType;
        }

        public async Task<bool> EditProductTypeAsync(ProductTypeInfor productType)
        {
            var productTypeEntity = await _context.ProductTypes.Where(x => x.Id == productType.Id && !x.IsDeleted).FirstOrDefaultAsync();
            if (productType.Name == productTypeEntity.Name)
            {
                throw new ProductTypeExistedException(productType.Name);
            }

            productTypeEntity.Name = productType.Name;
            productTypeEntity.IdBrand = productType.IdBrand;
            _context.ProductTypes.Update(productTypeEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BrandInfor>> GetListBrand()
        {
            var brands = await _context.Brands.Select(x => new BrandInfor
            {
                BrandName = x.Name,
                Id = x.Id,
            }).ToListAsync();
            return brands;
        }

        public async Task CreateProductTypeAsync(ProductTypeInfor productTypeInfor)
        {
            var productType = await _context.ProductTypes.Where(x => x.Name == productTypeInfor.Name && !x.IsDeleted).Select(x => x.Name).FirstOrDefaultAsync();
            if (productType == null)
            {
                var productTypeEntity = new ProductTypeEntity
                {
                    Name = productTypeInfor.Name,
                    IdBrand = productTypeInfor.IdBrand,
                };
                _context.ProductTypes.Add(productTypeEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ProductTypeExistedException(productTypeInfor.Name);
            }
        }

        public async Task<IPagedList<ProductDetailInfor>> GetListProductDetailAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listProductDetail = new List<ProductDetailInfor>();
            var productDetails = await _context.ProductDetails.Where(x => !x.IsDeleted).ToListAsync();
            if (productDetails != null && productDetails.Any())
            {
                foreach (var productDetail in productDetails)
                {
                    var productDetailInfor = new ProductDetailInfor
                    {
                        Id = productDetail.Id,
                        Name = productDetail.Name,
                        Pic1 = productDetail.Pic1,
                        Price = productDetail.Price,
                        Status = productDetail.Status,
                        IdProductType = productDetail.IdProductType,
                    };
                    listProductDetail.Add(productDetailInfor);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listProductDetail = listProductDetail.Where(s => s.Name.Contains(searchString)).ToList();
                }
                listProductDetail = sortOrder switch
                {
                    "name_desc" => listProductDetail.OrderByDescending(x => x.Name).ToList(),
                    "name" => listProductDetail.OrderBy(x => x.Name).ToList(),
                    "id_desc" => listProductDetail.OrderByDescending(x => x.Id).ToList(),
                    _ => listProductDetail.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listProductDetail.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<List<ProductTypeInfor>> GetListProductType()
        {
            var productTypes = await _context.ProductTypes.Where(x => !x.IsDeleted).Select(x => new ProductTypeInfor
            {
                Name = x.Name,
                Id = x.Id,
                IdBrand = x.IdBrand
            }).ToListAsync();

            return productTypes;
        }

        public async Task<IPagedList<ProductInfor>> GetListProductAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listProduct = new List<ProductInfor>();
            var products = await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    var productInfor = new ProductInfor
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Quantity = product.Quantity,
                        Size = product.Size,
                        IdProductDetail = product.IdProductDetail
                    };
                    listProduct.Add(productInfor);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listProduct = listProduct.Where(s => s.Name.Contains(searchString)).ToList();
                }
                listProduct = sortOrder switch
                {
                    "name_desc" => listProduct.OrderByDescending(x => x.Name).ToList(),
                    "name" => listProduct.OrderBy(x => x.Name).ToList(),
                    "id_desc" => listProduct.OrderByDescending(x => x.Id).ToList(),
                    _ => listProduct.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listProduct.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<List<ProductDetailInfor>> GetListProductDetail()
        {
            var productDetails = await _context.ProductDetails.Where(x => !x.IsDeleted).Select(x => new ProductDetailInfor
            {
                Name = x.Name,
                Id = x.Id,
                Pic1 = x.Pic1,
                Price = x.Price,
                Status = x.Status,
                IdProductType = x.IdProductType,
            }).ToListAsync();

            return productDetails;
        }
    }
}
