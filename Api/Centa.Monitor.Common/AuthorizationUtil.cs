using Centa.Monitor.Infrastructure.Model.System;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Centa.Monitor.Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class AuthorizationUtil
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="psw">原串</param>
        /// <param name="userKeyId">加密位</param>
        /// <returns></returns>
        public static string ToMD5(string psw, string userKeyId)
        {
            userKeyId = userKeyId.Replace("-", "");
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(userKeyId + psw); //Encoding.UTF8.GetBytes(str);//
            byte[] result = md5.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        // <summary>
        /// MD5加密 
        /// </summary>
        /// <param name="psw">原串</param>
        /// <returns></returns>
        public static string ToMD5(string psw)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(psw); //Encoding.UTF8.GetBytes(str);//
            byte[] result = md5.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取当前用户KeyId 
        /// </summary>
        /// <returns></returns>
        public static Guid GetCurrentUserKeyId()
        {
            return GetCurrentUserModel().KeyId;
        }

      

        /// <summary>
        /// 获取当前登陆用户信息
        /// </summary>
        /// <returns></returns>
        private static UserModel GetCurrentUserModel()
        {
            try
            {
                string identity = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                if (string.IsNullOrEmpty(identity))
                {
                    throw new Exception("无法获取到用户登录信息");
                }
                UserModel model = JsonConvert.DeserializeObject<UserModel>(identity);
                if (model == null)
                {
                    throw new Exception("无法获取到用户登录信息");
                }
                return model;
            }
            catch
            {
                throw new Exception("无法获取到用户登录信息");
            }
        }

        /// <summary>
        /// 获取当前请求IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentIPAddress()
        {
            string ip = string.Empty;
            try
            {
                string identity = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(identity))
                {
                    TokenInfo token = JsonConvert.DeserializeObject<TokenInfo>(identity);
                    ip = token.IPAddress;
                }
            }
            catch
            {
                throw new Exception("无法获取到用户登录信息");
            }
            return ip;
        }
    }

    public class TokenInfo
    {
        public string IPAddress { get; set; }
    }
}
