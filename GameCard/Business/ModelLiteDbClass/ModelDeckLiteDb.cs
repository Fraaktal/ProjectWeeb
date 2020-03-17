using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business.ModelLiteDbClass
{
    public class ModelDeckLiteDb
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<int> CardsIds { get; set; }
    }
}
