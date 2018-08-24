using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }
   
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Last time played")]
        public DateTime? LastPlayedAt { get; set; }

        public Hero()
        {
            CreatedAt = DateTime.UtcNow;
        }

    }
}