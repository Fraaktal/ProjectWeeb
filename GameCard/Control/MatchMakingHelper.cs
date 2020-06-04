using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;

namespace ProjectWeeb.GameCard.Control
{
    public static class MatchMakingHelper
    {
        public static HashSet<Game> MakeGameFromWaitingQueue(HashSet<Tuple<DateTime,Player>> players, int runningGameCount)
        {
            HashSet<Game> result = new HashSet<Game>();

            //Pas assez de joueur aucune partie ne peut commencer
            if (players.Count < 2)
            {
                return result;
            }

            //Pour l'instant on ne prendra en compte que le temps dans la file et pas le niveau, mais cela interviendra peut être par la suite.
            List<Tuple<DateTime, Player>> sortedByDatePlayer = players.OrderBy(p => p.Item1).ToList();

            int playerCount = sortedByDatePlayer.Count;

            if (playerCount % 2 == 1)
            {
                playerCount--;
            }

            int i = 0;
            int totalCount = runningGameCount;

            while (i < playerCount && totalCount < 10)
            {
                var game = CreateGame(sortedByDatePlayer.ElementAt(i).Item2, sortedByDatePlayer.ElementAt(i + 1).Item2);
                result.Add(game);
                i += 2;
                totalCount++;
            }
            
            return result;
        }

        private static Game CreateGame(Player p1,  Player p2)
        {
            Game game = new Game(p1,p2,$"{p1.Pseudo}_{p2.Pseudo}");

            return game;
        }
    }
}
