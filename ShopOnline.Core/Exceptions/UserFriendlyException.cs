using System;

namespace ShopOnline.Core.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {

        }
    }
}
