using ClassLibrary.Exceptions;
using ClassLibrary.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassLibrary.Entities
{
    public class GameplayActions : Entity
    {
        private Dictionary<string, string> actions = new Dictionary<string, string>();
        private Gameplay gameplay;
        private List<string> Messages = new List<string>();

        public GameplayActions(Gameplay gameplay)
        {
            this.gameplay = gameplay;
            actions.Add("go", "Go");
            actions.Add("run", "Go");
            actions.Add("move", "Go");
            actions.Add("attack", "Attack");
            actions.Add("hit", "Attack");
            actions.Add("revive", "Revive");
            actions.Add("suicude", "Revive");
            actions.Add("rest", "Rest");
            actions.Add("upgrade", "Upgrade");
        }

        public List<string> ExecuteAction(string action)
        {
            action = action.ToLower();
            string key = actions.Keys.Where(k => action.StartsWith(k)).FirstOrDefault();
            if (null == key || !actions.TryGetValue(key, out string methodName))
                throw new InvalidActionException($"Couldn't perform \"{action}\" action");


            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic|BindingFlags.Instance);
            action = action.Replace(key, "");

            List<Being> turnQueue = gameplay.Monsters.OfType<Being>().ToList();
            turnQueue.Add(gameplay.Player);
            foreach (Being b in turnQueue.OrderByDescending(b => b.Spd))
            {
                if (!b.IsDead())
                {
                    if (b is Hero)
                    {
                        method.Invoke(this, new string[] { action.Trim() });
                    }
                    else
                    {
                        Messages.Add($"You have been attacked {b.Name} for {b.AttackEnemy(gameplay.Player)} dmg");
                    }
                } else {
                    turnQueue.Remove(b);
                }
            }
            if (gameplay.Player.IsDead())
            {
                Revive("");
            }
            return Messages;
        }

        private void Go(string direction)
        {
            int x = gameplay.CurrentLocation.X, y = gameplay.CurrentLocation.Y;
            direction = direction.Replace("to the", "").Trim();
            switch (direction)
            {
                case "north":
                case "top":
                case "up":
                    y++;
                    break;
                case "south":
                case "bottom":
                case "down":
                    y--;
                    break;
                case "east":
                case "left":
                    x++;
                    break;
                case "west":
                case "right":
                    x--;
                    break;
            }
            gameplay.CurrentLocation = gameplay.World.LocationAt(x, y);
            gameplay.Monsters.Clear();
            Messages.Add($"You have moved to {gameplay.CurrentLocation.Name}. {gameplay.CurrentLocation.Description}");
        }

        private void Attack(string target)
        {
            if(0 == gameplay.Monsters.Count){
                Messages.Add($"You have tried to attack the ghost but unfortunately it was just your imagination.");
            } else {
                Monster enemy = gameplay.GetMonsterByName(target);

                Messages.Add($"You have attacked {enemy.Name} for {gameplay.Player.AttackEnemy(enemy)} dmg");

                if (enemy.IsDead()) {
                    gameplay.Monsters.Remove(enemy);
                    Messages.Add($"You have killed {enemy.Name} and earned {enemy.Exp} experience");
                    if (gameplay.Player.Exp < enemy.Exp) {
                        Messages.Add("Lvl up!");
                    }
                }
            }
        }

        private void PickUp(string target)
        {
            Messages.Add($"You have tried to pick up the {target} but it slipped and you can't find it anymore.");
        }
        private void Revive(string parameter)
        {
            gameplay.KillPlayer();
            Messages.Add($"You have died, but somehow your body was moved to the {gameplay.CurrentLocation.Name}. Where you were revived.");
        }

        private void Rest(string parameter)
        {
            gameplay.Player.ChangeHealth(2);
            Messages.Add($"You have restored 2 Health points while resting.");
        }
        private void Upgrade(string target)
        {
            if (gameplay.Player.TryUpgradeWeapon())
            {
                Messages.Add($"You have upgraded weapon to {gameplay.Player.WeaponLvl}.");
            } else
            {
                Messages.Add($"More gold is needed.");
            }
        }
    }
}