﻿using System;
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
            int totalDmg = r.Next(MinDmg, MaxDmg + 1) - enemy.PhysRes;

            if (totalDmg < 1) totalDmg = 1;
            enemy.ChangeHealth(-totalDmg);
            if (enemy.IsDead()) KillEnemy(enemy);
            return totalDmg;
        }

        public void KillEnemy(Being enemy)
        {
            RaiseExp(enemy.Exp);
        }

        public int GetRecalculatedStatValue(int dividor)
        {
            return GetRecalculatedStatValue(dividor, Lvl, Lvl - 1);
        }
        public int GetRecalculatedStatValue(int dividor, int currentValue)
        {
            return GetRecalculatedStatValue(dividor, currentValue, currentValue - 1);
        }

        public int GetRecalculatedStatValue(int dividor, int currentValue, int previousValue)
        {
            return (currentValue + previousValue) / dividor - (currentValue + previousValue) / dividor;
        }

        public void LvlUp()
        {
            LvlUp(1);
        }
        public void LvlUp(int numberofLvls)
        {
            int prevLvl = Lvl;
            Lvl += numberofLvls ;
            ExpToLvlUp = Lvl*Lvl * 3;
            int lvlDiffByOne = numberofLvls;
            int lvlDiffByTwo = GetRecalculatedStatValue(2, Lvl, prevLvl);
            int lvlDiffByThree = GetRecalculatedStatValue(3, Lvl, prevLvl);
            int lvlDiffByFour = GetRecalculatedStatValue(4, Lvl, prevLvl);
            int prevStr = Str;
            int prevDex = Dex;
            int prevSta = Sta;
            int prevInt = Int;

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
                MinDmg = GetRecalculatedStatValue(2,Str,prevStr);
                MaxDmg = MinDmg + GetRecalculatedStatValue(4, Str, prevStr);
            }
            else if (Class == Class.Archer)
            {
                MHP += lvlDiffByOne + lvlDiffByTwo;
                MMP += lvlDiffByOne;
                Dex += lvlDiffByTwo;
                MinDmg = GetRecalculatedStatValue(2, Dex, prevDex);
                MaxDmg = MinDmg + GetRecalculatedStatValue(4, Dex, prevDex);
            }
            else if (Class == Class.Mage)
            {
                MHP += lvlDiffByOne;
                MMP += lvlDiffByOne + lvlDiffByTwo;
                Int += lvlDiffByTwo;
                MinDmg = GetRecalculatedStatValue(2, Int, prevInt);
                MaxDmg = MinDmg + GetRecalculatedStatValue(4, Int, prevInt);
            }
            PhysRes += GetRecalculatedStatValue(3, Sta, prevSta);
            MHP += (Sta - prevSta) * 2;
            MMP += GetRecalculatedStatValue(2, Int, prevInt);
            AHP = MHP;
            AMP = MMP;
        }
    }
}