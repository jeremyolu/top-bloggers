using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TopBloggers.Interfaces.Services;

namespace TopBloggers.Services.Account
{
    public class SecurityService : ISecurirtyService
    {
        public string Encrypt(string password)
        {
            try
            {
                string ToReturn = "";
                string publicKey = "santhosh";
                string secretKey = "engineer";
                byte[] secretkeyByte = { };
                secretkeyByte = Encoding.UTF8.GetBytes(secretKey);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(publicKey);
                MemoryStream ms;
                CryptoStream cs;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(password);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public string Decrypt(string password)
        {
            try
            {
                string ToReturn = "";
                string publickey = "santhosh";
                string privatekey = "engineer";
                byte[] privatekeyByte = { };
                privatekeyByte = Encoding.UTF8.GetBytes(privatekey);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms;
                CryptoStream cs;
                byte[] inputbyteArray = new byte[password.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(password.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
    }
}