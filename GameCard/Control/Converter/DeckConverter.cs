using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control.Converter
{
    public class DeckConverter
    {
        public Deck ConvertBusiness(ModelDeckLiteDb modelDeck)
        {
            Deck resultDeck = new Deck
            {
                Id = modelDeck.Id, 
                IdUser = modelDeck.IdUser, 
                Name = modelDeck.Name, 
                Cards = GetCardsByIds(modelDeck.CardsIds)
            };

            return resultDeck;
        }

        

        public ModelDeckLiteDb ConvertToModel(Deck deck)
        {
            ModelDeckLiteDb resultDeck = new ModelDeckLiteDb
            {
                Name = deck.Name, 
                IdUser = deck.IdUser, 
                CardsIds = GetCardsIds(deck.Cards)
            };


            return resultDeck;
        }
        
        public ModelDeckLiteDb ConvertToModelUpdate(Deck deck)
        {
            ModelDeckLiteDb resultDeck = new ModelDeckLiteDb
            {
                Name = deck.Name, 
                IdUser = deck.IdUser, 
                Id = deck.Id, 
                CardsIds = GetCardsIds(deck.Cards)
            };

            return resultDeck;
        }

        private List<Card> GetCardsByIds(List<int> modelDeckCardsIds)
        {
            List<Card> result = new List<Card>();
            foreach (var id in modelDeckCardsIds)
            {
                Card card = CardManager.GetInstance().GetCardById(id);
                if (card != null)
                {
                    result.Add(card);
                }
            }

            return result;
        }

        private List<int> GetCardsIds(List<Card> deckCards)
        {
            List<int> result = new List<int>();

            foreach (var card in deckCards)
            {
                result.Add(card.CardId);
            }

            return result;
        }
    }
}
