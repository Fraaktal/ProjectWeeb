using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.EventArg;

namespace ProjectWeeb.GameCard.Manager
{
    public class GameManager
    {

        public event EventHandler<ConnectionEventArg> PlayerConnectedToGame;

        public GameManager()
        {
            Games = new List<Game>();
        }

        //ECOUTE DEVENT POUR CONNECTER LES PLAYER? LISTENCONTROLLER EVENT UNLISTENCONTROLLEREVENT
        
        public List<Game> Games { get; set; }

        private void ListenControllerEvent()
        {
            //GameEnded
        }

        private void UnListenControllerEvent()
        {

        }

        public string ConnectPlayerToGame(User user)
        {
            Player player = new Player(user.UserName, user.Level, user.Id, user.SelectedDeck);

            string id = TryJoinGame(player);
            if (id == null && Games.Count < 10)
            {
                id = GenerateGameId();
                Game game = new Game(player, id);
                Games.Add(game);
            }

            return id;
        }

        private string TryJoinGame(Player player)
        {
            foreach (var game in Games)
            {
                if (game.Player2 == null)
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

        public void RegisterConnectionId(int idUser, string gameId, string contextConnectionId)
        {
            Game game = Games.FirstOrDefault(g => g.GameId == gameId);

            if (game != null)
            {
                if (game.Player1.IdUser == idUser)
                {
                    game.Player1.ConnectionId = contextConnectionId;
                }
                else
                {
                    game.Player2.ConnectionId = contextConnectionId;
                }
            }
        }

        private string GenerateGameId()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
