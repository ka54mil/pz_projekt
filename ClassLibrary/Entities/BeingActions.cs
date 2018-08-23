using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ClassLibrary.Entities
{
    public partial class Being : Entity
    {
        public bool IsDead()
        {
            return 0 >= AHP;
        }

        public bool IsLvlUp()
        {
            return Exp >= ExpToLvlUp;
        }

        public void RaiseExp(int exp)
        {
            Exp += exp;
            LvlUp();
        }

        public bool LvlUp()
        {
            if (IsLvlUp())
            {
                Exp -= ExpToLvlUp;
                Lvl++;
                return true;
            }
            return false;
        }

        public int GetRecalculatedBy(int dividor)
        {
            return (Lvl + dividor-1) / dividor - (Lvl + dividor - 2) / dividor;
        }

        public void RecalculateLvlBasedStats()
        {
            ExpToLvlUp = Lvl*Lvl * 3;

            if (Race == Race.Beast)
            {
                Str += GetRecalculatedBy(3);
                Str += GetRecalculatedBy(4);
                MHP += 2;
            }
            else if (Race == Race.Elf)
            {
                MMP += 1;
                MHP += 1;
                Int++;
                Dex++;
            }
            else if (Race == Race.Human)
            {
                MMP += 1;
                MHP += 2;
                Int++;
                Sta++;
            }

            if (Class == Class.Warrior)
            {
                MHP += 3;
                MMP += 1;
                Str++;
                MinDmg = Str / 2;
                MaxDmg = MinDmg + Str / 4;
            }
            else if (Class == Class.Archer)
            {
                MHP += 2;
                MMP += 2;
                Dex++;
                MinDmg = Dex / 2;
                MaxDmg = MinDmg + Dex / 4;
            }
            else if (Class == Class.Mage)
            {
                MHP += 2;
                MMP += 2;
                Int++;
                MinDmg = Int / 2;
                MaxDmg = MinDmg + Int / 4;
            }
            PhysRes = (Sta) / 3;
            MHP += Sta * 2;
            MMP += Int;
            AHP = MHP;
            AMP = MMP;
        }

        public int AttackEnemy(Being enemy)
        {
            Random r = new Random();
            int totalDmg = r.Next(MinDmg, MaxDmg+1) - enemy.PhysRes;

            if(totalDmg < 1 ) totalDmg = 1;
            enemy.AHP -= totalDmg;
            if (enemy.IsDead()) KillEnemy(enemy);
            return totalDmg;
        }

        public void KillEnemy(Being enemy)
        {
            RaiseExp(enemy.Exp);
        }
        
    }
}