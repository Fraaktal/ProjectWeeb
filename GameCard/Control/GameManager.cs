using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ProjectWeeb.GameCard.Business;
using Microsoft.AspNetCore.SignalR.Client;

namespace ProjectWeeb.GameCard.Control
{
    public class GameManager
    {
        private static GameManager _instance;

        private GameManager()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/ChatHub")
                .Build();

            Connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };

            Connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                
            };

            

        }

        public static GameManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }

        public HubConnection Connection { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player CurrentTurnPlayer { get; set; }

        public async void Connect()
        {
            try
            {
                await Connection.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void SetUpGame()
        {

        }

        public void StartGame()
        {
            while (Player1.Hp > 0 && Player2.Hp > 0)
            {
                // On choisi le premier current player en random




                // Au tour du joueur suivante
                CurrentTurnPlayer = CurrentTurnPlayer == Player1 ? Player2 : Player1;
            }
        }
    }
}
