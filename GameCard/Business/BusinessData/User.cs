using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class User
    {
        public User()
        {

        }

        public User(string login, string password, int id,List<Card> cards) : this()
        {
            UserName = login;
            Password = password;
            Id = id;
            Level = 1;
            Cards = cards;
            Decks = new HashSet<Deck>();
        }

        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public int Level { get; set; }

        public HashSet<Deck> Decks { get; set; }

        public List<Card> Cards { get; set; }

        public Deck SelectedDeck { get; set; }
    }
}
