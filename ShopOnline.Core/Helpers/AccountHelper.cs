using System;
using System.Security.Cryptography;
using System.Text;

namespace ShopOnline.Core.Helpers
{
    public static class AccountHelper
    {
        public static string HashPassword(string password)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

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
