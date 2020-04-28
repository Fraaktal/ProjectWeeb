using System;
using System.Collections.Generic;
using ProjectWeeb.GameCard.Business;

namespace ProjectWeeb.GameCard.Manager
{
    public class EffectManager
    {
        private static EffectManager _instance;

        private EffectManager()
        {
            EffectsByIdCards = WeebResourceManager.GetInstance().GetEffectsByIdCardsFromFile();
        }

        public static EffectManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }

        public Dictionary<int,HashSet<Effect>> EffectsByIdCards { get; set; }

        public HashSet<Effect> GetEffectsByCardId(int id)
        {
            return EffectsByIdCards[id];
        }
    }
}
