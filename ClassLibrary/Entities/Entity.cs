using ClassLibrary.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ClassLibrary.Entities
{
    public abstract class Entity
    {
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                sb.Append($"{property.Name}: {property.GetValue(this)}\n");
            }

            return sb.ToString();
        }
        public string Serialize() => StringHelper.SerializeObject(this);

        public T Clone<T>() => StringHelper.DeserializeObject<T>(Serialize());
    }
}