using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class GameManager
    {
        public GameManager()
        {
            GameCommunicator = new GameCommunicator(this);

            GameController = new GameController(this);
        }

        public GameCommunicator GameCommunicator { get; set; }

        public GameController GameController { get; set; }
    }
}
