using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.Communication
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Context.ConnectionId; Id de l'appelant
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        
        public async Task PlayerConnectedOnGame(string idGame, string idUserS)
        {

            int idUser = int.Parse(idUserS);

            CWebSite.GetInstance().GameManager.RegisterConnectionId(idUser, idGame, Context.ConnectionId);

            string idConnectionOtherPlayer = CWebSite.GetInstance().GameManager.GetEnnemyConnectionId(idGame, idUser);

            if (idConnectionOtherPlayer != null)
            {
                await Clients.Clients(idConnectionOtherPlayer).SendAsync("OtherPlayerConnected");


            }

        }
    }
}
