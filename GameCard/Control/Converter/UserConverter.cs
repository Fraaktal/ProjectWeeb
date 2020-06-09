using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Control.DAO;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control.Converter
{
    public class UserConverter
    {
        public User ConvertToBusiness(ModelUserLiteDb modelUser)
        {
            User resultUser = new User();

            resultUser.UserName = modelUser.UserName;
            resultUser.Password = modelUser.Password; // ?
            resultUser.Decks = GetDecksByIds(modelUser.DecksIds);
            resultUser.Cards = GetCardsByIds(modelUser.CardsIds);
            resultUser.Level = modelUser.Level;
            resultUser.Id = modelUser.Id; 
            resultUser.SelectedDeck = CDeckDAO.GetInstance().GetDeck(modelUser.SelectedDeckId);

            return resultUser;
        }

        public ModelUserLiteDb ConvertToModel(User user)
        {
            ModelUserLiteDb resultUser = new ModelUserLiteDb();

            resultUser.UserName = user.UserName;
            resultUser.Password = user.Password;
            resultUser.DecksIds = GetDecksIds(user.Decks);
            resultUser.CardsIds = GetCardsIds(user.Cards);
            resultUser.Level = user.Level;
            resultUser.SelectedDeckId = user.SelectedDeck?.Id ?? -1;

            return resultUser;
        }
        
        public ModelUserLiteDb ConvertToModelUpdate(User user)
        {
            ModelUserLiteDb resultUser = new ModelUserLiteDb();

            resultUser.Id = user.Id;
            resultUser.UserName = user.UserName;
            resultUser.Password = user.Password;
            resultUser.DecksIds = GetDecksIds(user.Decks);
            resultUser.CardsIds = GetCardsIds(user.Cards);
            resultUser.Level = user.Level;
            resultUser.SelectedDeckId = user.SelectedDeck?.Id ?? -1;

            return resultUser;
        }

        private HashSet<Deck> GetDecksByIds(HashSet<int> modelUserDecksIds)
        {
            HashSet<Deck> result = new HashSet<Deck>();
            foreach (var id in modelUserDecksIds)
            {
                Deck deck = CDeckDAO.GetInstance().GetDeck(id);
                if (deck != null)
                {
                    result.Add(deck);
                }
            }

            return result;
        }

        private List<Card> GetCardsByIds(List<int> modelUserCardsIds)
        {
            List<Card> result = new List<Card>();
            foreach (var id in modelUserCardsIds)
            {
                Card card = CardManager.GetInstance().GetCardById(id);
                if (card != null)
                {
                    result.Add(card);
                }
            }

            return result;
        }

        private HashSet<int> GetDecksIds(HashSet<Deck> userDecks)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (var deck in userDecks)
            {
                result.Add(deck.Id);
            }

            return result;
        }

        private List<int> GetCardsIds(List<Card> userCards)
        {
            List<int> result = new List<int>();

            foreach (var card in userCards)
            {
                result.Add(card.CardId);
            }

            return result;
        }
    }
}
