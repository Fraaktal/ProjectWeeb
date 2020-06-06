using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.Communication
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Context.ConnectionId; Id de l'appelant
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        
        public async Task PlayerConnectedOnGame(string idGame)
        {
            // Context.ConnectionId; Id de l'appelant
        }
    }
}
