using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Business.ModelLiteDbClass;

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
            resultCard.ImagePath = SaveFileIfDoesNotExistAndGetPath(modelCard);

            return resultCard;
        }

        public ModelCardLiteDb ConvertToModel(Card card)
        {
            ModelCardLiteDb resultCard = new ModelCardLiteDb();

            resultCard.Base64Skin = GetBase64SkinFromPath(card.ImagePath);
            resultCard.Id = card.Id;
            resultCard.Name = card.Name;
            resultCard.EffectsIds = GetEffectsIds(card.Effects);

            return resultCard;
        }

        private string SaveFileIfDoesNotExistAndGetPath(ModelCardLiteDb modelCard)
        {
            string path = Path.Combine("localpath", modelCard.Name, ".png"); // todo localpath

            if (!File.Exists(path))
            {
                var bytes = Convert.FromBase64String(modelCard.Base64Skin);
                using (var imageFile = new FileStream(path, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }

            return path;
        }

        private HashSet<Effect> GetEffectFromIds(HashSet<int> modelCardEffectsIds)
        {
            HashSet<Effect> result = new HashSet<Effect>();
            foreach (var id in modelCardEffectsIds)
            {
                Effect effect = EffectManager.GetInstance().GetEffectById(id);
                if (effect != null)
                {
                    result.Add(effect);
                }
            }

            return result;
        }

        private HashSet<int> GetEffectsIds(HashSet<Effect> cardEffects)
        {
            HashSet<int> result = new HashSet<int>();
            foreach (var cardEffect in cardEffects)
            {
                result.Add(cardEffect.Id);
            }

            return result;
        }

        private string GetBase64SkinFromPath(string cardImagePath)
        {
            if (File.Exists(cardImagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(cardImagePath);
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }

            return null;
        }
    }
}
