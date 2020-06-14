using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class GameManager
    {
        public GameManager()
        {
            Games = new List<Game>();
        }

        public List<Game> Games { get; set; }

        public string ConnectPlayerToGame(User user)
        {
            Player player = new Player(user.UserName, user.Level, user.Id, user.SelectedDeck);

            string id = TryJoinGame(player);
            if (id == null && Games.Count < 10)
            {
                Game game = new Game(player);
                Games.Add(game);
                id = game.GameId;
            }

            return id;
        }

        private string TryJoinGame(Player player)
        {
            foreach (var game in Games)
            {
                if (game.Player1?.IdUser == player.IdUser || game.Player2?.IdUser == player.IdUser)
                {
                    return game.GameId;
                }
                if (game.Player2 == null && game.Player1?.IdUser != player.IdUser)
                {
                    game.JoinPlayer(player);
                    return game.GameId;
                }
            }

            return null;
        }

        public string GetEnnemyConnectionId(string gameId, int idUser)
        {
            return GetEnnemyPlayer(gameId, idUser)?.ConnectionId;
        }

        public Player GetPlayer(string idGame, in int idUser)
        {
            Game game = Games.FirstOrDefault(g => g.GameId == idGame);

            if (game != null)
            {
                if(game.Player1.IdUser == idUser)
                {
                    return game.Player1;
                }
                
                if (game.Player2.IdUser == idUser)
                {
                    return game.Player2;
                }
            }

            return null;
        }
        
        public Player GetEnnemyPlayer(string idGame, in int idUser)
        {
            Game game = Games.FirstOrDefault(g => g.GameId == idGame);

            if (game != null)
            {
                if(game.Player1.IdUser == idUser)
                {
                    return game.Player2;
                }
                
                if (game.Player2.IdUser == idUser)
                {
                    return game.Player1;
                }
            }

            return null;
        }

        public Game GetGame(string idGame)
        {
            return Games.FirstOrDefault(g => g.GameId == idGame);
        }
    }
}
