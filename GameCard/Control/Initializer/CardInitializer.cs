using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control.Initializer
{
    public class CardInitializer
    {
        public Dictionary<int, Card> InitializeCards()
        {
            return WeebResourceManager.GetInstance().GetCardsByIdFromFile();
        }
    }
}
