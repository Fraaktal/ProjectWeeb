using System;
using System.Collections.Generic;
using System.Timers;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.EventArg;

namespace ProjectWeeb.GameCard.Manager
{
    public class GameManager
    {

        public event EventHandler<ConnectionEventArg> PlayerConnectedToGame;

        public GameManager()
        {
            WaitingPlayers  = new HashSet<Tuple<DateTime, Player>>();

            Games = new HashSet<Game>();

            GameCommunicator = new GameCommunicator(this);

            GameController = new GameController(this);
            
            SetTimer();
        }

        //ECOUTE DEVENT POUR CONNECTER LES PLAYER? LISTENCONTROLLER EVENT UNLISTENCONTROLLEREVENT

        public HashSet<Tuple<DateTime,Player>> WaitingPlayers { get; set; }

        public HashSet<Game> Games { get; set; }

        public GameCommunicator GameCommunicator { get; set; }

        public GameController GameController { get; set; }
        
        private Timer Timer { get; set; }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer = new Timer(10000);

            // Hook up the Elapsed event for the timer. 
            Timer.Elapsed += LaunchGames;
            Timer.AutoReset = true;
            Timer.Enabled = true;

        }

        private void ListenControllerEvent()
        {
            //GameEnded
        }

        private void UnListenControllerEvent()
        {

        }

        public void ConnectPlayerToQueue(Player player)
        {
            Tuple<DateTime,Player> p = new Tuple<DateTime, Player>(DateTime.Now, player);
            WaitingPlayers.Add(p);
        }

        private void LaunchGames(object sender, ElapsedEventArgs e)
        {
             var gamesToAdd = MatchMakingHelper.MakeGameFromWaitingQueue(WaitingPlayers, Games.Count);

            foreach (var gameToAdd in gamesToAdd)
            {
                Games.Add(gameToAdd);


                WaitingPlayers.RemoveWhere(t => t.Item2 == gameToAdd.Player1 || t.Item2 == gameToAdd.Player2);
                
                PlayerConnectedToGame?.Invoke(this,null);
            }
        }
    }
}
