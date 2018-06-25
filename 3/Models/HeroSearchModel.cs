using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _3.Models
{
    public class HeroSearchModel : SearchModel
    {
        [RegularExpression(@"^[a-zA-Z0-9]*", ErrorMessage = "Only numbers and letters are allowed")]
        public string Name { get; set; }
    }
}