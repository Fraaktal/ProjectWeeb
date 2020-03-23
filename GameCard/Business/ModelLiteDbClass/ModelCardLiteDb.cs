using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.ModelLiteDbClass
{
    public class ModelCardLiteDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string,int> EffectsIds { get; set; }

        public string ImagePath { get; set; }
    }
}
