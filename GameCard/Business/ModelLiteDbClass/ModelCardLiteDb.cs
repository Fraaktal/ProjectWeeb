using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business.ModelLiteDbClass
{
    public class ModelCardLiteDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<int> EffectsIds { get; set; }

        public string Base64Skin { get; set; }
    }
}
