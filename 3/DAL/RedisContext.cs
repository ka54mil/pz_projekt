using ClassLibrary.Helpers;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.DAL
{
    public static class RedisContext
    {
        private static readonly String redisHost = "redis://redistogo:0a2062f5a7c30916e06e96779e0dd0be@soapfish.redistogo.com:9957";
        private static RedisManagerPool redisManager = new RedisManagerPool(redisHost);

        public static bool SaveToRedis(string key, Object value)
        {
            using (var redisClient = redisManager.GetClient())
            {
                return redisClient.Set(key, StringHelper.SerializeObject(value));
            }
        }
        public static T GetFromRedis<T>(string key)
        {
            using (var redisClient = redisManager.GetClient())
            {
                string value = redisClient.Get<string>(key);
                if (value == null) return default(T);
                return StringHelper.DeserializeObject<T>(value);
            }
        }
    }
}