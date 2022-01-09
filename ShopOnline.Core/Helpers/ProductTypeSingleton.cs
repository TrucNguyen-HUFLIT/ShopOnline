using ShopOnline.Core.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopOnline.Core.Helpers
{
    public sealed class ProductTypeSingleton
    {
        public static ProductTypeSingleton Instance { get; } = new ProductTypeSingleton();
        public List<TypeInforModel> ProductTypeInfors { get; } = new List<TypeInforModel>();
        private ProductTypeSingleton() { }
        private List<int> Ids { get; } = new List<int>();

        public void Init(List<TypeInforModel> productTypeInfors)
        {
            if (ProductTypeInfors.Count == 0 || ProductTypeInfors.Count != productTypeInfors.Count)
            {
                foreach (var productTypeInfor in productTypeInfors)
                {
                    var isExisted = Ids.Contains(productTypeInfor.Id);
                    if (!isExisted)
                    {
                        Ids.Add(productTypeInfor.Id);
                        ProductTypeInfors.Add(productTypeInfor);
                    }
                }

                var newIds = productTypeInfors.Select(x => x.Id).ToArray();
                var currentAmount = Ids.Count;

                for (int i = 0; i < currentAmount; i++)
                {
                    var isNotDeleted = newIds.Contains(Ids[i]);
                    if (!isNotDeleted)
                    {
                        ProductTypeInfors.Remove(ProductTypeInfors.Where(x => x.Id == Ids[i]).FirstOrDefault());
                        Ids.Remove(Ids[i]);
                    }
                }
            }
        }
    }
}
