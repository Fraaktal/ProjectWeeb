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
            GameManager = gameManager;
        }

        public GameManager GameManager { get; set; }





    }
}
