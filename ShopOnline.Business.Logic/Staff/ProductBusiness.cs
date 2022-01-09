using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Business.Staff;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Models.Enum;
using ShopOnline.Core.Models.Product;
using ShopOnline.Data.Repositories.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Logic.Staff
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly MyDbContext _context;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductBusiness(MyDbContext context, IWebHostEnvironment hostEnvironment, IProductDetailRepository productDetailRepository)
        {
            _context = context;
            _productDetailRepository = productDetailRepository;
            this.hostEnvironment = hostEnvironment;
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
                    };
                    listProductType.Add(productType);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listProductType = listProductType.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
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
            }).FirstOrDefault();

            return productType;
        }

        public async Task<bool> EditProductTypeAsync(ProductTypeInfor productType)
        {
            var productTypeEntity = await _context.ProductTypes.Where(x => x.Id == productType.Id && !x.IsDeleted).FirstOrDefaultAsync();
            var productTypeName = productType.Name.ToLower().Trim();

            if (productTypeEntity.Name.ToLower().Trim() != productTypeName)
            {
                bool isExistedProductTypeName = await _context.ProductTypes.AnyAsync(x => x.Name.ToLower().Trim() == productTypeName && !x.IsDeleted);
                if (isExistedProductTypeName) throw new UserFriendlyException(ErrorCode.ProductTypeExisted);
                productTypeEntity.Name = productType.Name;
                _context.ProductTypes.Update(productTypeEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task CreateProductTypeAsync(ProductTypeInfor productTypeInfor)
        {
            var productType = await _context.ProductTypes.Where(x => x.Name == productTypeInfor.Name && !x.IsDeleted).Select(x => x.Name).FirstOrDefaultAsync();
            if (productType == null)
            {
                var productTypeEntity = new ProductTypeEntity
                {
                    Name = productTypeInfor.Name,
                };
                _context.ProductTypes.Add(productTypeEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new UserFriendlyException(ErrorCode.ProductTypeExisted);
            }
        }

        public async Task<IPagedList<ProductDetailInfor>> GetListProductDetailAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listProductDetail = new List<ProductDetailInfor>();
            var productDetails = await _productDetailRepository.Get().ToListAsync();
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
                        BasePrice = productDetail.BasePrice,   
                        IdProductType = productDetail.IdProductType,
                    };
                    listProductDetail.Add(productDetailInfor);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listProductDetail = listProductDetail.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
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
            }).ToListAsync();

            return productTypes;
        }

        public async Task<List<ProductDetailInfor>> GetListProductDetail()
        {
            var productDetails = await _context.ProductDetails.Where(x => !x.IsDeleted && x.Status == AppEnum.ProductStatus.Available).Select(x => new ProductDetailInfor
            {
                Name = x.Name,
                Id = x.Id,
                Pic1 = x.Pic1,
                Price = x.Price,
                BasePrice = x.BasePrice,
                Status = x.Status,
                IdProductType = x.IdProductType,
            }).ToListAsync();

            return productDetails;
        }

        public async Task CreateProductDetailAsync(ProductDetailCreate productDetailCreate)
        {
            var productDetail = await _context.ProductDetails.Where(x => x.Name == productDetailCreate.Name && !x.IsDeleted).Select(x => x.Name).FirstOrDefaultAsync();
            if (productDetail == null)
            {
                var productDetailEntity = new ProductDetailEntity
                {
                    Name = productDetailCreate.Name,
                    Description = productDetailCreate.Description,
                    Status = productDetailCreate.Status,
                    Price = productDetailCreate.Price,
                    BasePrice = productDetailCreate.BasePrice,
                    Brand = productDetailCreate.Brand,
                    Quantity = productDetailCreate.Quantity,
                    IdProductType = productDetailCreate.IdProductType,
                };

                #region Save Image from wwwroot/img
                string wwwRootPath = hostEnvironment.WebRootPath;

                string fileName1 = Path.GetFileNameWithoutExtension(productDetailCreate.UploadPic1.FileName);
                string extension1 = Path.GetExtension(productDetailCreate.UploadPic1.FileName);
                productDetailEntity.Pic1 = "/img/Product/" + fileName1 + extension1;
                string path1 = Path.Combine(wwwRootPath + "/img/Product/", fileName1 + extension1);
                using (var fileStream = new FileStream(path1, FileMode.Create))
                {
                    await productDetailCreate.UploadPic1.CopyToAsync(fileStream);
                }

                if (productDetailCreate.UploadPic2 != null)
                {
                    string fileName2 = Path.GetFileNameWithoutExtension(productDetailCreate.UploadPic2.FileName);
                    string extension2 = Path.GetExtension(productDetailCreate.UploadPic2.FileName);
                    productDetailEntity.Pic2 = "/img/Product/" + fileName2 + extension2;
                    string path2 = Path.Combine(wwwRootPath + "/img/Product/", fileName2 + extension2);
                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await productDetailCreate.UploadPic2.CopyToAsync(fileStream);
                    }
                }

                if (productDetailCreate.UploadPic3 != null)
                {
                    string fileName3 = Path.GetFileNameWithoutExtension(productDetailCreate.UploadPic3.FileName);
                    string extension3 = Path.GetExtension(productDetailCreate.UploadPic3.FileName);
                    productDetailEntity.Pic3 = "/img/Product/" + fileName3 + extension3;
                    string path3 = Path.Combine(wwwRootPath + "/img/Product/", fileName3 + extension3);
                    using (var fileStream = new FileStream(path3, FileMode.Create))
                    {
                        await productDetailCreate.UploadPic3.CopyToAsync(fileStream);
                    }
                }
                #endregion

                _productDetailRepository.Add(productDetailEntity);
                await _productDetailRepository.SaveChangesAsync();
            }
            else
            {
                throw new UserFriendlyException(ErrorCode.ProductDetailExisted);
            }

        }

        public async Task<bool> UpdateProductDetailAsync(ProductDetailUpdate productDetailUpdate)
        {
            var productDetailEntity = await _context.ProductDetails.Where(x => x.Id == productDetailUpdate.Id && !x.IsDeleted).FirstOrDefaultAsync();
            var productDetailName = productDetailUpdate.Name.ToLower().Trim();

            if (productDetailEntity.Name.ToLower().Trim() != productDetailName)
            {
                bool isExistedProductDetailName = await _context.ProductDetails.AnyAsync(x => x.Name.ToLower().Trim() == productDetailName && !x.IsDeleted);
                if (isExistedProductDetailName) throw new UserFriendlyException(ErrorCode.ProductDetailExisted);
                productDetailEntity.Name = productDetailUpdate.Name;
                productDetailEntity.Description = productDetailUpdate.Description;
                productDetailEntity.Price = productDetailUpdate.Price;
                productDetailEntity.BasePrice = productDetailUpdate.BasePrice;
                productDetailEntity.Status = productDetailUpdate.Status;
                productDetailEntity.IdProductType = productDetailUpdate.IdProductType;
                productDetailEntity.Brand = productDetailUpdate.Brand;
                productDetailEntity.Quantity = productDetailUpdate.Quantity;

                #region Save Image from wwwroot/img
                string wwwRootPath = hostEnvironment.WebRootPath;

                string fileName1, fileName2, fileName3;
                string extension1, extension2, extension3;
                if (productDetailUpdate.UploadPic1 != null)
                {
                    fileName1 = Path.GetFileNameWithoutExtension(productDetailUpdate.UploadPic1.FileName);
                    extension1 = Path.GetExtension(productDetailUpdate.UploadPic1.FileName);
                    productDetailUpdate.Pic1 = "/img/Product/" + fileName1 + extension1;
                    string path1 = Path.Combine(wwwRootPath + "/img/Product/", fileName1 + extension1);
                    using (var fileStream = new FileStream(path1, FileMode.Create))
                    {
                        await productDetailUpdate.UploadPic1.CopyToAsync(fileStream);
                    }
                }
                if (productDetailUpdate.UploadPic2 != null)
                {
                    fileName2 = Path.GetFileNameWithoutExtension(productDetailUpdate.UploadPic2.FileName);
                    extension2 = Path.GetExtension(productDetailUpdate.UploadPic2.FileName);
                    productDetailUpdate.Pic2 = "/img/Product/" + fileName2 + extension2;
                    string path2 = Path.Combine(wwwRootPath + "/img/Product/", fileName2 + extension2);
                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await productDetailUpdate.UploadPic2.CopyToAsync(fileStream);
                    }
                }

                if (productDetailUpdate.UploadPic3 != null)
                {
                    fileName3 = Path.GetFileNameWithoutExtension(productDetailUpdate.UploadPic3.FileName);
                    extension3 = Path.GetExtension(productDetailUpdate.UploadPic3.FileName);
                    productDetailUpdate.Pic1 = "/img/Product/" + fileName3 + extension3;
                    string path3 = Path.Combine(wwwRootPath + "/img/Product/", fileName3 + extension3);
                    using (var fileStream = new FileStream(path3, FileMode.Create))
                    {
                        await productDetailUpdate.UploadPic3.CopyToAsync(fileStream);
                    }
                }

                #endregion

                _productDetailRepository.Update(productDetailEntity);
                await _productDetailRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public ProductDetailUpdate GetProductDetailByIdAsync(int id)
        {
            var productDetail = _context.ProductDetails.Where(x => x.Id == id && !x.IsDeleted).Select(x => new ProductDetailUpdate
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IdProductType = x.IdProductType,
                Pic1 = x.Pic1,
                Pic2 = x.Pic2,
                Pic3 = x.Pic3,
                Price = x.Price,
                Status = x.Status,
            }).FirstOrDefault();

            return productDetail;
        }

        public async Task<bool> DeleteProductTypeAsync(int id)
        {
            var productType = await _context.ProductTypes.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();

            if (productType != null)
            {
                var isCannotDelete = await _context.ProductDetails.Where(x => x.IdProductType == id).AnyAsync(x => !x.IsDeleted);

                if (isCannotDelete)
                {
                    throw new UserFriendlyException(ErrorCode.CannotDelete);
                }

                productType.IsDeleted = true;
                _context.ProductTypes.Update(productType);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductDetailAsync(int id)
        {
            var productDetail = await _context.ProductDetails.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();

            if (productDetail != null)
            {
                productDetail.IsDeleted = true;
                _productDetailRepository.SoftDelete(productDetail);
                await _productDetailRepository.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
