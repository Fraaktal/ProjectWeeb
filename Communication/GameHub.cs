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
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Essaie de connecter le player au jeu
        /// </summary>
        /// <param name="player">Joueur à connecter</param>
        /// <returns></returns>
        public async Task ConnectPlayer(Player player)
        {
            await CWebSite.GetInstance().GameManager.GameController.ConnectPlayer(player);
        }

        public async Task DisconnectPlayer(Player player)
        {
            await CWebSite.GetInstance().GameManager.GameController.DisconnectPlayer(player);
        }
    }
}
