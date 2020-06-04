using System;
using Microsoft.AspNetCore.SignalR.Client;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class GameCommunicator
    {
        public GameCommunicator(GameManager gameManager)
        {
            GameManager = gameManager;

            //TODO changer par la prod
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50322/GameHub")
                .Build();

#if DEBUG
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50322/GameHub")
                .Build();
#endif

        }

        public HubConnection Connection { get; set; }

        public GameManager GameManager { get; set; }

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
    }
}
