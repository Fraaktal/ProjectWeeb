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
                .WithUrl("https://localhost:44382/GameHub")
                .Build();
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
                if (Connection.State == HubConnectionState.Disconnected)
                {
                    await Connection.StartAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } 
        
        public async void Disconnect()
        {
            try
            {
                if (Connection.State == HubConnectionState.Connected)
                {
                    await Connection.StopAsync();
                }
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
