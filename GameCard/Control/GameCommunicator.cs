using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ProjectWeeb.GameCard.Control
{
    public class GameCommunicator
    {
        public GameCommunicator()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44382/GameHub")
                .Build();
        }

        public HubConnection Connection { get; set; }

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
