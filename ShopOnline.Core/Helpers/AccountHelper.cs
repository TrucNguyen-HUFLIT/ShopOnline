using System;

namespace ShopOnline.Core.Helpers
{
    public static class AccountHelper
    {
        public static string GetNewRandomPassword()
        {
            Random rnd = new();
            string value = "";
            for (int i = 0; i < 6; i++)
            {
                value += rnd.Next(0, 9).ToString();
            }
            return value;
        }
    }
}
