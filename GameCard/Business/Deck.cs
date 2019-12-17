using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business
{
    public class Deck
    {
        public Deck()
        {

        }

        public int IdDeck { get; set; }

        public List<Card> Cards { get; set; }
    }
}
