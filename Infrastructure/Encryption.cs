using System;
using System.Security.Cryptography;
using System.Text;

namespace Autosalon.Infrastructure;

 public class Encryption
    {
        public static string Encrypt(string password)
        {
            const string hash = "RoyalCars@cp2022$";
            var data = UTF8Encoding.UTF8.GetBytes(password);

            var md5 = new MD5CryptoServiceProvider();
            var tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            var transform = tripleDES.CreateEncryptor();
            var result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }


        public static string Dencrypt(string password)
        {
            const string hash = "RoyalCars@cp2022$";
            byte[] data = Convert.FromBase64String(password);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
    