using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;
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
            await GameManager.GetInstance().LogInPlayer(player);
        }

        public async Task DisconnectPlayer(Player player)
        {
            await GameManager.GetInstance().DisconnectPlayer(player);
        }

        public async Task PlayCard(Player player, Card card, Location location)
        {
            await GameManager.GetInstance().PlayCard(player, card, location);
        }

        public async Task DealDamageToOpponent(Card card, Player receiver)
        {
            await GameManager.GetInstance().DealDamageToOpponent(card, receiver);
        }
        
        public async Task DealDamageToCard(Player receiver, Card cardDealer, Card cardReceiver)
        {
            await GameManager.GetInstance().DealDamageToCard(receiver, cardDealer, cardReceiver);
        }

        public async Task ActivateCardEffect(Card card, Effect effect)
        {
            await GameManager.GetInstance().ActivateCardEffect(card, effect);
        }


    }
}
