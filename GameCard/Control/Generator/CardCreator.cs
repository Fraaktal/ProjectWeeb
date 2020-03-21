using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control.Generator
{
    public class CardCreator
    {
        public CardCreator()
        {

        }
        
        public Card CreateCardByIdAndName(int id, string name)
        {
            string path = WeebResourceManager.GetInstance().GetCardImageByCardId(id);
            var effects = EffectManager.GetInstance().GetEffectByCardId(id);

            Card result = new Card()
            {
                CardId = id,
                Name = name,
                Effects = effects,
                ImagePath = path
            };

            return result;
        }
    }
}