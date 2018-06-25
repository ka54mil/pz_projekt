using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Profile : Entity
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string UserName { get; set; }
        public ICollection<Hero> Heroes { get; set; }
    }
}