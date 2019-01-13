using ClassLibrary.Exceptions;
using ClassLibrary.Generators;
using ClassLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            actions.Add("walk", "Go");
            actions.Add("attack", "Attack");
            actions.Add("hit", "Attack");
            actions.Add("revive", "Revive");
            actions.Add("suicude", "Revive");
            actions.Add("rest", "Rest");
            actions.Add("eat", "Use");
            actions.Add("consume", "Use");
            actions.Add("use", "Use");
            actions.Add("upgrade", "Upgrade");
        }

        public List<string> ExecuteAction(string action)
        {
            action = action.ToLower();
            string key = actions.Keys.Where(k => action.StartsWith(k)).FirstOrDefault();
            if (null == key || !actions.TryGetValue(key, out string methodName))
                throw new InvalidActionException($"Couldn't perform \"{action}\" action");


            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic|BindingFlags.Instance);
            action = action.ReplaceFirst(key, "");

            List<Being> turnQueue = gameplay.Monsters.OfType<Being>().ToList();
            turnQueue.Add(gameplay.Player);
            foreach (Being b in turnQueue.OrderByDescending(b => b.Spd))
            {
                if (!b.IsDead())
                {
                    if (b is Hero)
                    {
                        method.Invoke(this, new object[] { action.Trim() });
                    }
                    else
                    {
                        Messages.Add($"You have been attacked by {b.Name} for {b.AttackEnemy(gameplay.Player)} dmg, now you have {gameplay.Player.AHP} HP.");
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
            Dictionary<string, string[]> directions = new Dictionary<string, string[]>();
            directions.Add("north",new string[] { "north","up", "top"});
            directions.Add("south",new string[] { "north", "bottom", "down" });
            directions.Add("east", new string[] { "east", "left" });
            directions.Add("west", new string[] { "west", "right" });
            string result = directions.Where(w => w.Value.Any(direction.Contains)).Select(w => w.Key).FirstOrDefault();

            switch (result)
            {
                case "north":
                    y++;
                    break;
                case "south":
                    y--;
                    break;
                case "east":
                    x++;
                    break;
                case "west":
                    x--;
                    break;
                default:
                    Messages.Add($"Unknown direction, please try another");
                    return;
            }
            gameplay.CurrentLocation = gameplay.World.LocationAt(x, y);
            gameplay.Monsters.Clear();
            Messages.Add($"You have moved {result} to {gameplay.CurrentLocation.Name}. {gameplay.CurrentLocation.Description}");
        }

        private void Attack(string target)
        {
            if(0 == gameplay.Monsters.Count){
                Messages.Add($"You have tried to attack the ghost but unfortunately it was just your imagination.");
            } else {
                Monster enemy = gameplay.GetMonsterByName(target);

                Messages.Add($"You have attacked {enemy.Name} for {gameplay.Player.AttackEnemy(enemy)} dmg.");

                if (enemy.IsDead()) {
                    gameplay.Monsters.Remove(enemy);
                    var items = RandomHelper.GetDroppedItems(enemy);
                    Messages.Add($"You have killed {enemy.Name} and earned {enemy.Exp} experience.");
                    if (items.Count > 0)
                    {
                        gameplay.Player.AddItemsToPockets(items);
                        string dropList = String.Join(", ", items.Select(i => i.ItemInfo.Name).ToArray());
                        Messages.Add($"{enemy.Name} has dropped {dropList} so you took it.");
                    }
                    if (gameplay.Player.Exp < enemy.Exp) {
                        Messages.Add("Lvl up! Your HP is fully restored.");
                    }
                }
            }
        }

        private void PickUp(string target)
        {
            Messages.Add($"You tried to pick up the {target} but you couldn't find it.");
        }
        private void Revive(string parameter)
        {
            gameplay.KillPlayer();
            Messages.Add($"You have died, but somehow your body was moved to the {gameplay.CurrentLocation.Name}. Where you were revived.");
        }

        private void Rest(string parameter)
        {
            gameplay.Player.ChangeAHP(2);
            Messages.Add($"You have restored 2 HP, now you have {gameplay.Player.AHP} HP.");
        }

        private void Use(string target)
        {
            var pocket = gameplay.Player.Pockets.Where(p => p.Item.ItemInfo.Name.Equals(target, StringComparison.OrdinalIgnoreCase)).LastOrDefault();
            if (object.Equals(pocket, null))
            {
                Messages.Add($"You don't have {target}.");
            } else
            {
                pocket.Item.Use(gameplay.Player);
                Messages.Add($"You have consumed {target}, now you have {gameplay.Player.AHP} HP.");
                if(pocket.Item.Quantity == 1)
                {
                    gameplay.Player.Pockets.Remove(pocket);
                } else
                {
                    pocket.Item.Quantity--;
                }
            }
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