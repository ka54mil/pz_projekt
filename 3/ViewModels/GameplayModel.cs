using ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.ViewModels
{
    public class GameplayModel
    {
        public Hero Player { get; set; }
        public string[] Messages { get; set; }

        public GameplayModel()
        {

        }

        public GameplayModel(Hero player, string[] messages)
        {
            Player = player;
            Messages = messages;
        }
    }
}