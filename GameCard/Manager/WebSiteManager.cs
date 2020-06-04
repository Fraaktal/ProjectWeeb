using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class WebSiteManager
    {
        public WebSiteManager()
        {
            UserController = new UserController(this);
        }

        public UserController UserController { get; set; }
    }
}
