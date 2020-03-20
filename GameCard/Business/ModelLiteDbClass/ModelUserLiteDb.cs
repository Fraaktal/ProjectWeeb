using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business.ModelLiteDbClass
{
    public class ModelUserLiteDb
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public int Level { get; set; }

        public HashSet<int> DecksIds { get; set; }

        public List<int> CardsIds { get; set; }
    }
}
