using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control.DAO;

namespace ProjectWeeb.GameCard.Control.Generator
{
    public class CardGenerator
    {
        public CardGenerator()
        {
            GameCards = CheckAndCreateCardIfInexistant();
            ExistingCards = InitializeExistingCards();

            CardCreator = new CardCreator();
        }

        public CardCreator CardCreator { get; set; }

        public Dictionary<int, string> ExistingCards { get; }
        
        private Dictionary<int, string> InitializeExistingCards()
        {
            Dictionary<int, string> result = new Dictionary<int, string>
            {
                {0, "Megumin"},
                {1, "SeeleVollerei"},
                {2, "Joker"},
                {3, "IronMan"},
                {4, "SaikiKusuo"},
                {5, "LightYagami"},
                {6, "Guts"},
                {7, "AllMight"},
                {8, "Monika"},
                {9, "Sans"},
                {10, "Pikachu"},
                {11, "Sonic"},
                {12, "Zelda"},
                {13, "RoyMustang"},
                {14, "Asuna"},
                {15, "Shiro"},
                {16, "Aqua"},
                {17, "YunaGasai"},
                {18, "Mikasa"},
                {19, "MakiseKurisu"}
            };

            return result;
        }

        public HashSet<Card> GameCards { get; }

        public HashSet<Card> CheckAndCreateCardIfInexistant()
        {
            HashSet<Card> result = new HashSet<Card>();

            foreach (var existingcard in ExistingCards)
            {
                Card card = CCardDAO.GetInstance().GetCard(existingcard.Key);

                if (card == null)
                {
                    card = CardCreator.CreateCardByIdAndName(existingcard.Key, existingcard.Value);
                    CCardDAO.GetInstance().SaveCard(card);
                }

                result.Add(card);
            }

            return result;
        }
    }
}