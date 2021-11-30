using System;

namespace ShopOnline.Core.Exceptions
{
    public class BrandExistedException : SystemException
    {
        public BrandExistedException(string brand)
          : base($" Brand is already ")
        {

        }
    }
}
