using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control.Converter
{
    public class CardConverter
    {
        public Card ConvertBusiness(ModelCardLiteDb modelCard)
        {
            Card resultCard = new Card();

            resultCard.Id = modelCard.Id;
            resultCard.Name = modelCard.Name;
            resultCard.Effects = GetEffectFromIds(modelCard.EffectsIds);
            resultCard.ImagePath = modelCard.ImagePath;

            return resultCard;
        }

        public ModelCardLiteDb ConvertToModel(Card card)
        {
            ModelCardLiteDb resultCard = new ModelCardLiteDb();

            resultCard.ImagePath = card.ImagePath;
            resultCard.Id = card.Id;
            resultCard.Name = card.Name;
            resultCard.EffectsIds = GetEffectsIds(card.Effects);

            return resultCard;
        }

        private Dictionary<string, Effect> GetEffectFromIds(Dictionary<string, int> modelCardEffectsIds)
        {
            Dictionary<string, Effect> result = new Dictionary<string, Effect>();
            foreach (var effectDb in modelCardEffectsIds)
            {
                Effect effect = EffectManager.GetInstance().GetEffectById(effectDb.Value);
                if (effect != null && !result.ContainsKey(effectDb.Key))
                {
                    result.Add(effectDb.Key, effect);
                }
            }

            return result;
        }

        private Dictionary<string, int> GetEffectsIds(Dictionary<string, Effect> cardEffects)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (var cardEffect in cardEffects)
            {
                if (!result.ContainsKey(cardEffect.Key))
                {
                    result.Add(cardEffect.Key, cardEffect.Value.Id);
                }
            }

            return result;
        }
    }
}
