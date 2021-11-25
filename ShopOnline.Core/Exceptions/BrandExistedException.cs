using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Exceptions
{
    public class BrandExistedException: SystemException
    {
        public BrandExistedException(string brand)
          : base($" Brand is already ")
        {

        }
    }
}
