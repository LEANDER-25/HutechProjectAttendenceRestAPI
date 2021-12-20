using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RESTAPIRNSQLServer.Extensions
{
    public static class Tool
    {
        public static string MD5Hash(this string password)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}