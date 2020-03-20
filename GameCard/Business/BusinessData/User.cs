using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class User
    {
        public User()
        {
            Decks = new HashSet<Deck>();
            Cards = new List<Card>();
        }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public int Level { get; set; }

        public HashSet<Deck> Decks { get; set; }

        public List<Card> Cards { get; set; }
    }
}
