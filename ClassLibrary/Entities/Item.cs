using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Item : Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Item()
        {

        }

        public Item(string name)
        {
            Name = name;
        }
    }
}