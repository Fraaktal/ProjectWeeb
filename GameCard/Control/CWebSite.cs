using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class CWebSite
    {
        private static CWebSite _instance;

        private CWebSite()
        {
            WebSiteManager = new WebSiteManager();

            GameManager = new GameManager();
        }
        
        public static CWebSite GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CWebSite();
            }

            return _instance;
        }

        public WebSiteManager WebSiteManager { get; set; }

        public GameManager GameManager { get; set; }

        public int Value { get; set; }

    }
}
