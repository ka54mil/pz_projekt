using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Hero : Being
    {
        public int ProfileID { get; set; }

        public virtual Profile Profiles { get; set; }
    }
}