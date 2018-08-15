using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        public Hero()
        {
            CreatedAt = DateTime.UtcNow;
        }
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }

        public virtual Profile Profile { get; set; }
   
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last time played")]
        public DateTime? LastPlayedAt { get; set; }
    }
}