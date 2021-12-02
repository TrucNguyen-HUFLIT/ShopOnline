using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Exceptions
{
    public class ProductDetailExistedException :SystemException
    {
        public ProductDetailExistedException(string productDetail)
              : base($" Product Detail is already ")
        {

        }
    }
}
