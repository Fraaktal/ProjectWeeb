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
            
            await Clients.Clients(idGame).SendAsync("PlayerConnected", idUser,Context.ConnectionId);
        }

        
        public async Task InitializeGame(string connectionId, int life, string name,int[][] handCards, string nameOtherPlayer, int otherPlayerLife)
        {
            await Clients.Client(connectionId).SendAsync("InitializeGamePlayerSide",
                name,life, handCards, nameOtherPlayer, otherPlayerLife);
        }

        public async Task EnemyConnected(string connectionId, int life, string name)
        {
            await Clients.Client(connectionId).SendAsync("OtherPlayerConnected", name, life);
        }

        public async Task SetPlayerTurn(string connectionIdP1, string connectionIdP2, int[][] p1handCards, int[][] p2handCards)
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

        public async Task CardSuccesfullyPlayed(string connectionIdP1, string connectionIdP2, int[][] pside, int[][] handCards)
        {
            await Clients.Client(connectionIdP1).SendAsync("PlayerCardPlayed", handCards, pside);
            await Clients.Client(connectionIdP2).SendAsync("EnemyCardPlayedClient", pside.Reverse());
        }

        public async Task AttackCard(string idGame, string idUserS, int positionOrigin, int positionTargeted)
        {
            int idUser = int.Parse(idUserS);

            await Clients.Client(idGame).SendAsync("AttackCard", idUser, positionOrigin, positionTargeted);
        }

        public async Task CardAttacked(string idPlayerOrigin, string idPlayerTargeted, int[][] originSide, int[][] targetSide)
        {
            await Clients.Client(idPlayerOrigin).SendAsync("CardAttacked", originSide, targetSide.Reverse());
            await Clients.Client(idPlayerTargeted).SendAsync("CardAttacked", targetSide, originSide.Reverse());
        }

        public async Task AttackEnemy(string idGame, string idUserS, int positionOrigin)
        {
            int idUser = int.Parse(idUserS);

            await Clients.Client(idGame).SendAsync("AttackEnemy", idUser, positionOrigin);
        }
        
        public async Task PlayerAttacked(string idPlayerOrigin, string idPlayerTargeted, int newLifeTarget)
        {
            await Clients.Client(idPlayerOrigin).SendAsync("EnemyAttacked", newLifeTarget);
            await Clients.Client(idPlayerTargeted).SendAsync("PlayerAttacked", newLifeTarget);
        }
        
        public async Task EndGame(string idWinner, string idLoser)
        {
            await Clients.Client(idWinner).SendAsync("GameWon");
            await Clients.Client(idLoser).SendAsync("GameLost");
        }
    }
}
