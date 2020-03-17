using LiteDB;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class WebSiteManager
    {
        public WebSiteManager()
        {
            DatabaseController = new DatabaseController(this);

            UserController = new UserController(this);
        }

        public DatabaseController DatabaseController { get; set; }

        public UserController UserController { get; set; }

        public User CurrentUser { get; set; }
    }
}
