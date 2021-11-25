using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Exceptions
{
    public class EmailException : SystemException
    {
        public EmailException(string email)
            : base($" Email is already ")
        {

        }
    }
}
