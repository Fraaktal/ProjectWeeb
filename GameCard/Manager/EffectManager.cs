using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
