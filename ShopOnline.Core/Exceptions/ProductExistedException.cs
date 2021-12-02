using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Exceptions
{
    public class ProductExistedException: SystemException
    {
        public ProductExistedException(string product)
              : base($" Product Name is already ")
        {

        }
    }
}
