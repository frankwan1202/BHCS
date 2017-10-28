using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastCommon.Serializes;
using BHCS.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Common
{
    public class HttpCookieHelper
    {
        private static readonly IJsonSerializer _jsonSerializer = ObjectContainer.Resolve<IJsonSerializer>();

        /// <summary>
        /// 判断Cookie是否存在
        /// </summary>
        /// <param name="key">Cookie 名称</param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            string cookie = MvcContext.Context.Request.Cookies[key];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 添加或修改Cookie
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="key">Cookie 名称</param>
        /// <param name="value">Cookie 值</param>
        /// <param name="expires">Cookie过期时间，默认为86400秒（即1天）</param>
        /// <param name="encrypted">是否加密存储Cookie值，true：加密，false：不加密</param>
        public static void AddOrUpdate<T>(string key, T value, long? expires = 86400, bool encrypted = false) where T : class
        {
            CookieOptions options = new CookieOptions();
            options.HttpOnly = true;
            string objStrValue = _jsonSerializer.Serialize(value);
            if (expires.HasValue)
            {
                options.Expires = DateTime.Now.AddSeconds(expires.Value);
            }

            if (MvcContext.Context.Request.Cookies[key] != null)
            {
                MvcContext.Context.Response.Cookies.Delete(key);
            }

            MvcContext.Context.Response.Cookies.Append(key, objStrValue, options);
        }


        /// <summary>
        /// 添加或修改Cookie
        /// </summary>
        /// <param name="key">Cookie名称</param>
        /// <returns></returns>
        [Obsolete("只允许存储基础类型参数，该方法为明码存储，请注意安全性！")]
        public static void AddOrUpdateForBaseType(string key, string value, int? expires = 86400)
        {
            CookieOptions options = new CookieOptions();

            if (expires.HasValue)
            {
                options.Expires = DateTime.Now.AddSeconds(expires.Value);
            }

            if (MvcContext.Context.Request.Cookies[key] != null)
            {
                MvcContext.Context.Response.Cookies.Delete(key);
            }

            MvcContext.Context.Response.Cookies.Append(key, value, options);
        }

        /// <summary>
        /// 添加或修改Cookie
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="key">Cookie 名称</param>
        /// <param name="value">Cookie 值</param>
        /// <param name="encrypted">是否加密存储Cookie值，true：加密，false：不加密</param>
        //public static void AddOrUpdate<T>(string key, T value, bool encrypted = false) where T : class
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(key);
        //    }

        //    string objStrValue = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value), System.Text.Encoding.UTF8);
        //    if (encrypted)
        //    {
        //        objStrValue = DESEncrypt.Encrypt(objStrValue);
        //    }
        //    cookie.Value = objStrValue;
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            string cookie = MvcContext.Context.Request.Cookies[key];
            if (cookie != null)
            {
                MvcContext.Context.Response.Cookies.Delete(key);
            }
            //HttpContext.Current.Response.Cookies.Remove(key);
        }

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="key">Cookie名称</param>
        /// <param name="decrypted">是否要解密Cookie值，true：解密，false：不解密</param>
        /// <returns></returns>
        public static T Get<T>(string key, bool decrypted = false) where T : class
        {
            CookieOptions options = new CookieOptions();


            string cookie = MvcContext.Context.Request.Cookies[key];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie))
            {
                return null;
            }

            T obj = _jsonSerializer.Deserialize<T>(cookie);

            return obj;
        }

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="key">Cookie名称</param>
        /// <returns></returns>
        [Obsolete("只允许存储基础类型参数，该方法为明码存储，请注意安全性！")]
        public static string GetForBaseType(string key)
        {
            string cookie = MvcContext.Context.Request.Cookies[key];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie))
            {
                return null;
            }

            return cookie;
        }
    }
}
