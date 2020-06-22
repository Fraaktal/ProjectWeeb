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
            CardInitializer generator = new CardInitializer();
            CardsById = generator.InitializeCards();
        }


        public static CardManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CardManager();
            }

            return _instance;
        }

        public Dictionary<int,Card> CardsById { get; set; }

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

            for (int i = 0; i < 20; i++)
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