using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Effect : Entity
    {
        public int ID { get; set; }
        [Required]
        public float Duration { get; set; }
        [Required]
        public float Power { get; set; }
        [Required]
        public EffectType EffectType { get; set; }
        public virtual ICollection<Being> Beings { get; set; }
    }

    public enum EffectType{
        undefined,
        stun,
        heal,
        dmg
    }
}
