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
            
            await Clients.Clients(idGame).SendAsync("PlayerConnected", idUser,Context.ConnectionId);
        }

        
        public async Task InitializeGame(string connectionId, int[] handCards)
        {
            await Clients.Client(connectionId).SendAsync("InitializeGamePlayerSide", handCards);
        }
        
        public async Task SetPlayerTurn(string connectionIdP1, string connectionIdP2, int[] p1handCards, int[] p2handCards)
        {
            Clients.Client(connectionIdP1).SendAsync("TurnChanged", true, p1handCards);
            await Clients.Client(connectionIdP2).SendAsync("TurnChanged", false, p2handCards);
        }
        
        public async Task LaunchTimer(string idGame)
        {
            await Clients.Client(idGame).SendAsync("SetTimer");
        }
        
        public async Task AskForNextTurn(string idGame)
        {
            await Clients.Client(idGame).SendAsync("SetNextTurn");
        }

        public async Task PlayCard(string idGame, string idUserS, int idCard, int position, int positionInHand)
        {
            int idUser = int.Parse(idUserS);

            await Clients.Client(idGame).SendAsync("CardPlayed", idUser, idCard, position, positionInHand);
        }

        public async Task CardSuccesfullyPlayed(string connectionIdP1, string connectionIdP2, int[] pside, int[] handCards)
        {
            await Clients.Client(connectionIdP1).SendAsync("PlayerCardPlayed", handCards, pside);
            await Clients.Client(connectionIdP2).SendAsync("EnemyCardPlayedClient", pside.Reverse());
        }
    }
}
