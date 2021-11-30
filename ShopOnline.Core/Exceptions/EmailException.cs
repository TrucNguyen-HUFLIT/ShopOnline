using System;

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
