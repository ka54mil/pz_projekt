using _3.Models;
using ClassLibrary.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace _3.Helpers
{
    public static class ListHelper
    {
        public static List<T> SortByProperty<T>(this List<T> list, string sortOrder, string sortProperty )
        {
            if ("desc".Equals(sortOrder))
            {
                if (typeof(T).GetProperty(sortProperty) != null)
                {
                    list = list.OrderByDescending(h => h.GetType().GetProperty(sortProperty).GetValue(h)).ToList();
                }
            }
            else
            {
                if (typeof(T).GetProperty(sortProperty) != null)
                {
                    list = list.OrderBy(h => h.GetType().GetProperty(sortProperty).GetValue(h)).ToList();
                }
            }
            return list;
        }

        public static List<T> SearchByProperties<T>(this List<T> list, SearchModel entity)
        {
            IEnumerable<T> result = list;
            foreach(PropertyInfo p in entity.GetType().GetProperties())
            {
                var value = p.GetValue(entity);
                if (value!= null && !value.Equals("") && !value.Equals(0))
                {
                    result = result.Where(e => {

                        var val = e.GetType().GetProperty(p.Name).GetValue(e);
                        if (val != null)
                        {
                            if (p.PropertyType.Equals(typeof(string)))
                            {
                                return val.ToString().Contains(value.ToString());
                            }
                            return val.Equals(value);
                        } else {
                            return false;
                        }
                            
                    });
                }
            }
            return result.ToList();
        }
    }
}