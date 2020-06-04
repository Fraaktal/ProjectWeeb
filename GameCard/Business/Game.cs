using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business
{
    public class Game
    {
        public Game(Player p1, Player p2, string gameId)
        {
            Player1 = p1;
            Player2 = p2;
            GameId = gameId;
        }

        public string GameId { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public BattleField BattleField { get; set; }

        public void InitializeGame()
        {
            Player1.Hp = 20;
            Player2.Hp = 20;

            BattleField = new BattleField();

            ListenControllerEvent();
        }

        public void EndGame()
        {
            UnListenControllerEvent();
        }

        public void ListenControllerEvent()
        {
            //ECOUTE DEVENEMENT COMMUNICATION
        }
        
        public void UnListenControllerEvent()
        {
            //DESECOUTE
        }
    }
}
