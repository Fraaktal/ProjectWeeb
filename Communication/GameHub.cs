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
        //NOT GOOD GERER DANS LE GAME
        public async Task PlayerConnectedOnGame(string idGame, string idUserS)
        {
            int idUser = int.Parse(idUserS);

            Game game = CWebSite.GetInstance().GameManager.GetGame(idGame);

            await Clients.Clients(game.GameId).SendAsync("PlayerConnected", idUser,Context.ConnectionId);
        }

        
        public async Task InitializeGame(string connectionId, int[] handCards)
        {
            await Clients.Client(connectionId).SendAsync("InitializeGamePlayerSide", handCards);
        }
        
        public async Task SetPlayerTurn(string connectionIdP1, string connectionIdP2)
        {
            Clients.Client(connectionIdP1).SendAsync("TurnChanged", true);
            await Clients.Client(connectionIdP2).SendAsync("TurnChanged", false);
        }
        
        public async Task LaunchTimer(string idGame)
        {

        }
    }
}
