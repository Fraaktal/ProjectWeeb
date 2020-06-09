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
        public async Task PlayerConnectedOnGame(string idGame, string idUserS)
        {
            int idUser = int.Parse(idUserS);

            CWebSite.GetInstance().GameManager.RegisterConnectionId(idUser, idGame, Context.ConnectionId);

            string idConnectionOtherPlayer = CWebSite.GetInstance().GameManager.GetEnnemyConnectionId(idGame, idUser);

            if (idConnectionOtherPlayer != null)
            {
                await Clients.Clients(idConnectionOtherPlayer).SendAsync("OtherPlayerConnected");
            }

            Player player = CWebSite.GetInstance().GameManager.GetPlayer(idGame, idUser);

           await Clients.Clients(Context.ConnectionId).SendAsync("ReceivePlayerCard", player.DrawPile);
        }
    }
}
