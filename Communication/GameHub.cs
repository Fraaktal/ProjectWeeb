using System;
using System.Linq;
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

            int[] drawpile = new int[player.DrawPile.Count];
            for (int i = 0; i < player.DrawPile.Count; i++)
            {
                drawpile[i] = player.DrawPile.ElementAt(i).CardId;
            }

            await Clients.Clients(player.ConnectionId).SendAsync("InitializeGamePlayerSide", drawpile);

            Game game = CWebSite.GetInstance().GameManager.GetGame(idGame);

            if (game.Player1 != null && game.Player2 != null)
            {
                Clients.Clients(game.Player1.ConnectionId).SendAsync("GameReadyToStart", true);
                await Clients.Clients(game.Player2.ConnectionId).SendAsync("GameReadyToStart", false);
            }

        }

        
        public async Task LaunchTimer(string idGame, string idUserS)
        {

        }
    }
}
