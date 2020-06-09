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

            User user = new User(login, password, -1, cards);

            if (!CUserDAO.GetInstance().DoesUserExist(login))
            {
                CUserDAO.GetInstance().RegisterUser(user);

                user = CUserDAO.GetInstance().GetUserByLoginAndPassword(login, password);

                Deck deck = new Deck(user.Cards, "DefaultDeck", user.Id);

                CDeckDAO.GetInstance().SaveDeck(deck);

                deck = CDeckDAO.GetInstance().GetDeckByUserIdAndName(user.Id, "DefaultDeck");

                user.Decks.Add(deck);
                user.SelectedDeck = deck;

                bool result = CUserDAO.GetInstance().UpdateUser(user);

                return result;
            }

            return false;
        }
    }
}
