﻿using ACBC.Buss;
using Newtonsoft.Json;
using Senparc.CO2NET.Cache.Redis;
using Senparc.Weixin.WxOpen.Containers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ACBC.Common
{
    public class Utils
    {
        public static string GetOpenID(string token)
        {
            SessionBag sessionBag = SessionContainer.GetSession(token);
            if (sessionBag == null)
            {
                return null;
            }
            return sessionBag.OpenId;
        }

        public static string GetMemberID(string token)
        {
            SessionBag sessionBag = SessionContainer.GetSession(token);
            if (sessionBag == null)
            {
                return null;
            }
            SessionUser sessionUser = JsonConvert.DeserializeObject<SessionUser>(sessionBag.Name);
            if(sessionUser == null)
            {
                return null;
            }
            return sessionUser.memberId;
        }

        public static bool SetCache(string key, object value, int hours, int minutes, int seconds)
        {
            key = Global.NAMESPACE + "." + key;
            var db = RedisManager.Manager.GetDatabase(Global.REDIS_NO);
            var expiry = new TimeSpan(hours, minutes, seconds);
            string valueStr = JsonConvert.SerializeObject(value);
            return db.StringSet(key, valueStr, expiry);
        }

        public static bool SetCache(BussCache value, int hours, int minutes, int seconds)
        {
            string key = value.GetType().FullName + value.Unique;
            return SetCache(key, value, hours, minutes, seconds);
        }

        public static bool SetCache(BussCache value)
        {
            return SetCache(value, Global.REDIS_EXPIRY_H, Global.REDIS_EXPIRY_M, Global.REDIS_EXPIRY_S);
        }

        public static dynamic GetCache<T>(string key) 
        {
            key = Global.NAMESPACE + "." + key;
            var db = RedisManager.Manager.GetDatabase(Global.REDIS_NO);
            if(db.StringGet(key).HasValue)
            {
                return JsonConvert.DeserializeObject<T>(db.StringGet(key));
            }
            return null;
        }

        public static dynamic GetCache<T>() where T : BussCache
        {
            string key = typeof(T).FullName;
            return GetCache<T>(key);
        }

        public static dynamic GetCache<T>(BussParam bussParam) where T : BussCache
        {
            string key = typeof(T).FullName + bussParam.GetUnique();
            return GetCache<T>(key);
        }

        public static void DeleteCache(string key)
        {
            key = Global.NAMESPACE + "." + key;
            var db = RedisManager.Manager.GetDatabase(Global.REDIS_NO);
            if (db.StringGet(key).HasValue)
            {
                db.KeyDelete(key);
            }
        }

        public static void DeleteCacheAll(string key)
        {
            key = Global.NAMESPACE + "." + key + "*";
            var db = RedisManager.Manager.GetDatabase(Global.REDIS_NO);
            var server = RedisManager.Manager.GetServer(Global.REDIS);
            var keys = server.Keys(database: db.Database, pattern: key);
            db.KeyDelete(keys.ToArray());
        }

        public static void DeleteCache<T>(bool delChild = false) where T : BussCache
        {
            string key = typeof(T).FullName;
            if(delChild)
            {
                DeleteCacheAll(key);
            }
            else
            {
                DeleteCache(key);
            }
        }

        public static void DeleteCache<T>(BussParam bussParam) where T : BussCache
        {
            string key = typeof(T).FullName + bussParam.GetUnique();
            DeleteCache(key);
        }

        public static void ClearCache()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(a => a.GetTypes()
                  .Where(t => typeof(BussCache).Equals(t.BaseType)))
                  .ToArray();
            foreach (var v in types)
            {
                if (v.IsClass)
                {
                    DeleteCacheAll(v.FullName);
                }
            }
        }

        public static string PostHttp(string url, string body, string contentType)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;

            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }

        public static string GetHttp(string url)
        {

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 20000;

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();

            return responseContent;
        }

        public static double GetExchange(string name)
        {
            ExchangeRes exchangeRes = GetCache<ExchangeRes>("EXCHANGE");
            if(exchangeRes == null)
            {
                string exchange = GetHttp(Global.EXCHANGE_URL);
                exchangeRes = JsonConvert.DeserializeObject<ExchangeRes>(exchange);
                SetCache("EXCHANGE", exchangeRes, 1, 0, 0);
            }

            double exRate = 0;
            if (exchangeRes.error_code == 0)
            {
                var list = exchangeRes.result.list;
                foreach (string[] item in list)
                {
                    foreach (string s in item)
                    {
                        if (s != name)
                            break;
                        exRate = Convert.ToDouble(item[3]);
                        break;
                    }
                    if (exRate > 0)
                        break;
                }
            }

            return exRate / 100;
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        public static string FilterChar(string input)
        {
            Regex r = new Regex("^[0-9]{1,}$"); //正则表达式 表示数字的范围 ^符号是开始，$是关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in input)
            {
                if (item >= 0x4e00 && item <= 0x9fbb)//汉字范围
                {
                    sb.Append(item);
                }

                if (Regex.IsMatch(item.ToString(), @"[A-Za-z0-9]"))
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

    }
}
