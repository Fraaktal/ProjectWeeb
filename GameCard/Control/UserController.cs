using System.Collections.Generic;
using System.Net;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control.DAO;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class UserController
    {
        public UserController(WebSiteManager manager)
        {
            WebSiteManager = manager;
        }

        public WebSiteManager WebSiteManager { get; set; }

        public User TryLogUser(string login, string password)
        {
            var result = CUserDAO.GetInstance().GetUserByLoginAndPassword(login, password);

            return result;
        }

        public bool TryRegisterUser(string login, string password)
        {
            var cards = CardManager.GetInstance().GenerateWelcomingCard();

            User user = new User(login, password, -1, cards, new HashSet<Deck>());

            if (!CUserDAO.GetInstance().DoesUserExist(login))
            {
                CUserDAO.GetInstance().RegisterUser(user);

                return true;
            }

            return false;
        }
    }
}
