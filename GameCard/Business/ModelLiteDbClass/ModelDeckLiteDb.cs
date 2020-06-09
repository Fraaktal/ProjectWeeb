using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.ModelLiteDbClass
{
    public class ModelDeckLiteDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IdUser { get; set; }

        public List<int> CardsIds { get; set; }
    }
}
