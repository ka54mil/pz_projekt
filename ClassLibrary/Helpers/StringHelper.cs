﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers
{
    public static class StringHelper
    {
       public static string ToUppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] c = s.ToCharArray();
            c[0] = char.ToUpper(c[0]);
            return new string(c);
        }

        public static string SerializeObject(Object toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
        }
        public static T DeserializeObject<T>(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(toDeserialize);
        }
    }
}
