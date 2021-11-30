using System;

namespace ShopOnline.Core.Exceptions
{
    public class ProductTypeExistedException : SystemException
    {
        public ProductTypeExistedException(string productType)
              : base($" Product Type is already ")
        {

        }
    }
}
