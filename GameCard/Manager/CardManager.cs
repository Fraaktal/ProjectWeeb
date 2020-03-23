using System.Collections.Generic;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Manager
{
    public class CardManager
    {
        private static CardManager _instance;

        private CardManager()
        {

        }

        public static CardManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CardManager();
            }

            return _instance;
        }

        public Card GetCardById(int id)
        {
            return null;
        }

        public List<Card> GenerateWelcomingCard()
        {
            return null;
        }
    }
}