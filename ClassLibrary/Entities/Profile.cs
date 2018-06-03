using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Profile : Entity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public ICollection<Hero> Heroes { get; set; }
    }
}