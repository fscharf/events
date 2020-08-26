using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Events
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:44390/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static string GetHash(string pswd)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pswd));

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}