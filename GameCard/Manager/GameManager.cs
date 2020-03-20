using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class GameManager
    {
        private static GameManager _instance;

        private GameManager()
        {
            GameCommunicator = new  GameCommunicator();

            GameController = new GameController();
        }

        public static GameManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }

        public GameCommunicator GameCommunicator { get; set; }

        public GameController GameController { get; set; }
    }
}
