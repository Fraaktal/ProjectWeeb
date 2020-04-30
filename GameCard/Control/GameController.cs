using System;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class GameController
    {
        public GameController(GameManager gameManager)
        {
            BattleField = new BattleField();

            GameManager = gameManager;
        }

        public GameManager GameManager { get; set; }

        public BattleField BattleField { get; set; }

        public Player CurrentPlayer { get; set; }

        public Player Ennemy { get; set; }

        public Player CurrentTurnPlayer { get; set; }

        public void SetUpGame()
        {

        }

        public void StartGame()
        {
            while (CurrentPlayer.Hp > 0 && Ennemy.Hp > 0)
            {
                // On choisi le premier current player en random

                // Au tour du joueur suivante
                CurrentTurnPlayer = CurrentTurnPlayer == CurrentPlayer ? Ennemy : CurrentPlayer;
            }
        }

        public async Task ConnectPlayer(Player player)
        {
            if (CurrentPlayer == null)
            {
                CurrentPlayer = player;
            }
            else if (Ennemy == null)
            {
                Ennemy = player;
            }
            else
            {
                //TODO error
            }
        }

        public async Task DisconnectPlayer(Player player)
        {
            if (CurrentPlayer.Equals(player))
            {
                CurrentPlayer = null;
            }
            else if (Ennemy.Equals(player))
            {
                Ennemy = null;
            }
        }
    }
}
