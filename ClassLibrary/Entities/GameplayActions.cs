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
        }

        public string ExecuteAction(string action)
        {
            action = action.ToLower();
            string key = actions.Keys.Where(k => action.StartsWith(k)).FirstOrDefault();
            string methodName = null;
            if (null == key || !actions.TryGetValue(key, out methodName)) return $"Couldn't perform \"{action}\" action";

            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.NonPublic|BindingFlags.Instance);
            action = action.Replace(key, "");
            return method.Invoke(this, new string[] { action.Trim()}).ToString();
        }

        private string Go(string direction)
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
            return $"You have moved to {gameplay.CurrentLocation.Name}. {gameplay.CurrentLocation.Description}";
        }

        private string Attack(string target)
        {
            if(0 == gameplay.Monsters.Count)
            {
                return $"You have tried to attack the ghost but unfortunately it was just your imagination.";
            } else
            {
                if ("".Equals(target))
                {
                    return $"You have attacked {gameplay.Monsters.First()}";
                }
                else
                {
                    return $"You have attacked {target}";
                }
            }
        }

        private string PickUp(string target)
        {
            return $"You have tried to pick up the {target} but it slipped and you can't find it anymore.";
        }
    }
}