using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Entities
{
    public class Pocket : Entity
    {
        public static int MaxPocketSize = 20;
        public int ID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int BeingID { get; set; }

        public virtual Item Item { get; set; }
        public virtual Being Being { get; set; }
    }
}