using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Manager
{
    public class DeckManager
    {
        private static DeckManager _instance;

        private DeckManager()
        {

        }

        public static DeckManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DeckManager();
            }

            return _instance;
        }

        public Deck GetDeckById(int id)
        {
            return null;
        }
    }
}