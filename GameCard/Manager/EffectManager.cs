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

        }

        public static EffectManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EffectManager();
            }

            return _instance;
        }

        public Effect GetEffectById(int id)
        {
            return null;
        }

        public Dictionary<string, Effect> GetEffectByCardId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
