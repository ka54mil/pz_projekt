using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using ClassLibrary.Helpers;

namespace ClassLibrary.Entities
{
    public partial class Being : Entity
    {
        public bool IsDead()
        {
            return 0 >= AHP;
        }

        public void ChangeHealth(int health)
        {
            AHP = health + AHP < MHP ? health + AHP : MHP;
        }
        
        public void ChangeMana(int mana)
        {
            AMP = mana + AMP < MMP ? mana + AMP : MMP;
        }

        public bool IsLvlUp()
        {
            return Exp >= ExpToLvlUp;
        }

        public void RaiseExp(int exp)
        {
            Exp += exp;
            if (IsLvlUp())
            {
                Exp -= ExpToLvlUp;
                LvlUp();
            }
        }
        
        public int AttackEnemy(Being enemy)
        {
            Random r = new Random();
            var dmg = r.Next(MinDmg, MaxDmg + 1);
            dmg = enemy.TakeDmg(dmg);
            if (enemy.IsDead()) KillEnemy(enemy);
            return dmg;
        }

        public int TakeDmg(int dmg)
        {
            dmg -= PhysRes;
            if (dmg < 1) dmg = 1;
            ChangeHealth(-dmg);
            return dmg;
        }

        public void KillEnemy(Being enemy)
        {
            RaiseExp(enemy.Exp);
            Gold += enemy.Gold;
        }

        public void LvlUp()
        {
            LvlUp(1);
        }
        public void LvlUp(int numberOfLvls)
        {
            var prevLvl = Lvl;
            Lvl += numberOfLvls;
            ExpToLvlUp = Lvl*Lvl * 3;
            var lvlDiffByOne = numberOfLvls;
            var lvlDiffByTwo = StatsHelper.GetRecalculatedStatValue(2, Lvl, prevLvl);
            var lvlDiffByThree = StatsHelper.GetRecalculatedStatValue(3, Lvl, prevLvl);
            var lvlDiffByFour = StatsHelper.GetRecalculatedStatValue(4, Lvl, prevLvl);
            var prevStr = Str;
            var prevDex = Dex;
            var prevSta = Sta;
            var prevInt = Int;

            if (Race == Race.Beast)
            {
                Str += lvlDiffByFour;
                Sta += lvlDiffByThree;
                MHP += lvlDiffByOne * 2;
            }
            else if (Race == Race.Elf)
            {
                MMP += lvlDiffByOne;
                MHP += lvlDiffByOne;
                Int += lvlDiffByThree;
                Dex += lvlDiffByFour;
            }
            else if (Race == Race.Human)
            {
                MHP += lvlDiffByOne;
                Int += lvlDiffByFour;
                Sta += lvlDiffByThree;
                Dex += lvlDiffByFour;
            }

            if (Class == Class.Warrior)
            {
                MHP += lvlDiffByOne * 2;
                MMP += lvlDiffByTwo;
                Str += lvlDiffByTwo;
                MinDmg += StatsHelper.GetRecalculatedStatValue(3,Str,prevStr);
                MaxDmg += StatsHelper.GetRecalculatedStatValue(2, Str, prevStr);
            }
            else if (Class == Class.Archer)
            {
                MHP += lvlDiffByOne + lvlDiffByTwo;
                MMP += lvlDiffByOne;
                Dex += lvlDiffByTwo;
                MinDmg += StatsHelper.GetRecalculatedStatValue(3, Dex, prevDex);
                MaxDmg += StatsHelper.GetRecalculatedStatValue(2, Dex, prevDex);
            }
            else if (Class == Class.Mage)
            {
                MHP += lvlDiffByOne;
                MMP += lvlDiffByOne + lvlDiffByTwo;
                Int += lvlDiffByTwo;
                MinDmg += StatsHelper.GetRecalculatedStatValue(3, Int, prevInt);
                MaxDmg += StatsHelper.GetRecalculatedStatValue(2, Int, prevInt);
            }
            PhysRes += StatsHelper.GetRecalculatedStatValue(3, Sta, prevSta);
            MHP += (Sta - prevSta) * 2;
            MMP += StatsHelper.GetRecalculatedStatValue(2, Int, prevInt);
            AHP = MHP;
            AMP = MMP;
        }

        public void Revive()
        {
            AHP = (MHP + 8) / 10;
            AMP = (MMP + 8) / 10;
        }
    }
}