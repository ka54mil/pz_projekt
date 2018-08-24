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

        public GameplayActions(Gameplay gameplay)
        {
            this.gameplay = gameplay;
            actions.Add("go", "Go");
            actions.Add("run", "Go");
            actions.Add("move", "Go");
            actions.Add("attack", "Attack");
            actions.Add("hit", "Attack");
        }

        public string[] ExecuteAction(string action)
        {
            action = action.ToLower();
            string key = actions.Keys.Where(k => action.StartsWith(k)).FirstOrDefault();
            string methodName = null;
            if (null == key || !actions.TryGetValue(key, out methodName)) return new string[] { $"Couldn't perform \"{action}\" action" };

            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic|BindingFlags.Instance);
            action = action.Replace(key, "");
            return method.Invoke(this, new string[] { action.Trim()}) as string[];
        }

        private string[] Go(string direction)
        {
            int x = gameplay.CurrentLocation.X;
            int y = gameplay.CurrentLocation.Y;
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
            return new string[] { $"You have moved to {gameplay.CurrentLocation.Name}. {gameplay.CurrentLocation.Description}"};
        }

        private string[] Attack(string target)
        {
            List<string> result = new List<string>();
            if(0 == gameplay.Monsters.Count)
            {
                result.Add($"You have tried to attack the ghost but unfortunately it was just your imagination.");
            } else
            {
                Monster enemy = null;
                if (!"".Equals(target))
                {
                    enemy = gameplay.Monsters.Where(e => e.Name.Equals(target)).FirstOrDefault();
                }
                if(null == enemy)
                {
                    enemy = gameplay.Monsters.First();
                }
                int dmgDone = gameplay.Player.AttackEnemy(enemy);
                result.Add($"You have attacked {enemy.Name} for {dmgDone} dmg");
                if (enemy.IsDead())
                {
                    result.Add($"You have killed {enemy.Name} and earned {enemy.Exp} experience");
                    if (gameplay.Player.Exp > enemy.Exp)
                    {
                        result.Add("Lvl up!");
                    }
                }
            }
            return result.ToArray();
        }

        private string PickUp(string target)
        {
            return $"You have tried to pick up the {target} but it slipped and you can't find it anymore.";
        }
    }
}