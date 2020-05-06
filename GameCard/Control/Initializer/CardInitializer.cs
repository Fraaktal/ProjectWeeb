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
        public Card CreateCardByIdAndName(int id, string name)
        {
            string path = WeebResourceManager.GetInstance().GetCardImageByCardId(id);
            var effects = EffectManager.GetInstance().GetEffectsByCardId(id);

            Card result = new Card(id, name, effects, path);

            return result;
        }

        public Dictionary<int, Card> InitializeCards(Dictionary<int, string> cardsNameById)
        {
            Dictionary<int, Card> result = new Dictionary<int, Card>();

            foreach (var cardById in cardsNameById)
            {
                var card = CreateCardByIdAndName(cardById.Key, cardById.Value);

                result.Add(card.CardId,card);
            }

            return result;
        }
    }
}
