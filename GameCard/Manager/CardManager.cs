using System;
using System.Collections.Generic;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control.Initializer;

namespace ProjectWeeb.GameCard.Manager
{
    public class CardManager
    {
        private static CardManager _instance;

        private CardManager()
        {
            CardsNameById = InitializeCardsNameById();
            CardInitializer generator = new CardInitializer();
            CardsById = generator.InitializeCards(CardsNameById);
        }


        public static CardManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CardManager();
            }

            return _instance;
        }

        public Dictionary<int, string> CardsNameById { get; set; }

        public Dictionary<int,Card> CardsById { get; set; }

        private Dictionary<int, string> InitializeCardsNameById()
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

        public Card GetCardById(int id)
        {
            Card card = null;

            if (CardsById.ContainsKey(id))
            {
                card = CardsById[id];
            }

            return card;
        }

        // Génère 20 cartes lors de l'inscription
        public List<Card> GenerateWelcomingCard()
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < 19; i++)
            {
                cards.Add(GenerateRandomCard());
            }

            return cards;
        }

        public Card GenerateRandomCard()
        {
            Random rand = new Random();
            int index = (int)(rand.NextDouble()*19);

            return GetCardById(index);
        }
    }
}