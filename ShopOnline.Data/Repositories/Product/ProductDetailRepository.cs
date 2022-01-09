using ShopOnline.Core;
using ShopOnline.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Repositories.Product
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly MyDbContext _context;

        public ProductDetailRepository(MyDbContext context)
        {
            _context = context;
        }

        public IQueryable<ProductDetailEntity> Get()
        {
            return _context.ProductDetails.Where(x => !x.IsDeleted);
        }

        public void Add(ProductDetailEntity productDetailEntity)
        {
            _context.ProductDetails.Add(productDetailEntity);
        }

        public void Update(ProductDetailEntity productDetailEntity)
        {
            _context.ProductDetails.Update(productDetailEntity);
        }

        public void SoftDelete(ProductDetailEntity productDetailEntity)
        {
            productDetailEntity.IsDeleted = true;
            _context.ProductDetails.Update(productDetailEntity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
