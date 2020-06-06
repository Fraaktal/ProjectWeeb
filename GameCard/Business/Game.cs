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
            Date = DateTime.Now;

            BattleField = new BattleField();
            ListenControllerEvent();
        }
        
        public Game(Player p1, string gameId)
        {
            Player1 = p1;
            GameId = gameId;
            Date = DateTime.Now;

            BattleField = new BattleField();
            ListenControllerEvent();
        }

        

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public string GameId { get; set; }

        public DateTime Date { get; set; }
        
        public BattleField BattleField { get; set; }

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

        public void JoinPlayer(Player player)
        {
            Player2 = player;
            //TODO INFORM
        }
    }
}
