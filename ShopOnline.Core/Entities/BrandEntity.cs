using System.Collections.Generic;

namespace ShopOnline.Core.Entities
{
    public class BrandEntity : BaseEntity
    {
        public string BrandName { get; set; }
        public bool? Status { get; set; }
        public virtual ICollection<ProductTypeEntity> ProductTypes { get; set; }
    }
}
