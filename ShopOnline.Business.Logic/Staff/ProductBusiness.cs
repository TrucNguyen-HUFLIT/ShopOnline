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
using System.Text;
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
            var brand = await _context.Brands.Where(x => x.Name == brandCreate.BrandName && !x.IsDelete).Select(x=>x.Name).FirstOrDefaultAsync();
            if(brand==null)
            {
                var brandEntity = new BrandEntity
                {
                    Name = brandCreate.BrandName,
                };
                _context.Add(brand);
                await _context.SaveChangesAsync();
            }    
            else if(brand!=null)
            {
                throw new BrandExistedException(brand);
            }    
        }

        public async Task<bool> EditBrandAsync(BrandInfor brandInfor)
        {
            var brandEntity = await _context.Brands.Where(x => x.Id == brandInfor.Id && !x.IsDelete).FirstOrDefaultAsync();
            if(brandEntity.Name == brandInfor.BrandName)
            {
                throw new BrandExistedException(brandInfor.BrandName);
            }    
            brandEntity.Name = brandInfor.BrandName;

            _context.Update(brandEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public BrandInfor GetBrandByIdAsync(int id)
        {
            var brand = _context.Brands.Where(x => x.Id == id && !x.IsDelete).Select(x => new BrandInfor
            {
                Id = x.Id,
                BrandName = x.Name,
            }).FirstOrDefault();

            return brand;
        }

        public async Task<IPagedList<BrandInfor>> GetListBrandAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listBrand = new List<BrandInfor>();
            var brands = await _context.Brands.Where(x => !x.IsDelete).ToListAsync();
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
            var productTypes = await _context.ProductTypes.Where(x => !x.IsDelete).ToListAsync();
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
            var productType = _context.ProductTypes.Where(x => x.Id == id && !x.IsDelete).Select(x => new ProductTypeInfor
            {
                Id = x.Id,
                Name = x.Name,
                IdBrand= x.IdBrand,
            }).FirstOrDefault();

            return productType;
        }

        public async Task<bool> EditBrandAsync(ProductTypeInfor productTypeInfor)
        {
            var productType = await _context.ProductTypes.Where(x => x.Id == productTypeInfor.Id && !x.IsDelete).FirstOrDefaultAsync();

            productType.Name = productTypeInfor.Name;
            productType.IdBrand = productType.IdBrand;
            _context.Update(productType);
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
    }
}
