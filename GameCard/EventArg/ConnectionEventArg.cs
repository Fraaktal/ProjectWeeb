using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;

namespace ProjectWeeb.GameCard.EventArg
{
    public class ConnectionEventArg
    {
        public ConnectionEventArg(Player player,Game game)
        {
            Player = player;
            Game = game;
        }

        public Player Player { get; set; }

        public Game Game { get; set; }
    }
}
