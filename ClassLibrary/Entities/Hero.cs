using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }

        public virtual Profile Profiles { get; set; }
    }
}