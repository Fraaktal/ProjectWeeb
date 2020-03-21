using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.Manager;

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

        public async Task PlayCard(Player player, Card card)
        {
            await CWebSite.GetInstance().GameManager.GameController.PlayCard(player, card);
        }

        public async Task DealDamageToOpponent(Card card, Player receiver)
        {
            await CWebSite.GetInstance().GameManager.GameController.DealDamageToOpponent(card, receiver);
        }
        
        public async Task DealDamageToCard(Player receiver, Card cardDealer, Card cardReceiver)
        {
            await CWebSite.GetInstance().GameManager.GameController.DealDamageToCard(receiver, cardDealer, cardReceiver);
        }

        public async Task ActivateCardEffect(Card card, Effect effect)
        {
            await CWebSite.GetInstance().GameManager.GameController.ActivateCardEffect(card, effect);
        }
    }
}
