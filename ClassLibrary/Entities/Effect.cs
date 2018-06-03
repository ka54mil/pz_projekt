using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Entities
{
    public class Effect : Entity
    {
        public int ID { get; set; }
        public float Duration { get; set; }
        public float Power { get; set; }
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
