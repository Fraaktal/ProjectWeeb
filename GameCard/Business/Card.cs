using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business
{
    public class Card
    {
        public Card()
        {

        }

        public string Name { get; set; }

        public int IdCard { get; set; }

        public List<Effect> Effects { get; set; }
    }
}
