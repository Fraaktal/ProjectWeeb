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
            Level = 1;
            Decks = new HashSet<Deck>();
            Cards = new List<Card>();
        }

        public User(string login, string password, List<Card> cards): this()
        {
            UserName = login;
            Password = password;
            Cards.AddRange(cards);
        }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public int Level { get; set; }

        public HashSet<Deck> Decks { get; set; }

        public List<Card> Cards { get; set; }
    }
}
