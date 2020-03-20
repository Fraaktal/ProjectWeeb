using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Control
{
    public class GameController
    {
        public GameController()
        {
            BattleField = new BattleField();
        }

        public BattleField BattleField { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player CurrentTurnPlayer { get; set; }

        public void SetUpGame()
        {

        }

        public void StartGame()
        {
            while (Player1.Hp > 0 && Player2.Hp > 0)
            {
                // On choisi le premier current player en random

                // Au tour du joueur suivante
                CurrentTurnPlayer = CurrentTurnPlayer == Player1 ? Player2 : Player1;
            }
        }

        public async Task LogInPlayer(Player player)
        {
            if (Player1 == null)
            {
                Player1 = player;
            }
            else if (Player2 == null)
            {
                Player2 = player;
            }
            else
            {
                //TODO error
            }
        }

        public async Task DisconnectPlayer(Player player)
        {
            if (Player1.Equals(player))
            {
                Player1 = null;
            }
            else if (Player2.Equals(player))
            {
                Player2 = null;
            }
        }

        public async Task PlayCard(Player player, Card card/*Location location*/)
        {

        }

        public async Task DealDamageToOpponent(Card card, Player receiver)
        {
            if (Player1.Equals(receiver))
            {

            }
            else if (Player2.Equals(receiver))
            {
                Player2 = null;
            }
        }

        public async Task DealDamageToCard(Player receiver, Card cardDealer, Card cardReceiver)
        {
            throw new NotImplementedException();
        }

        public async Task ActivateCardEffect(Card card, Effect effect)
        {
            throw new NotImplementedException();
        }
    }
}
