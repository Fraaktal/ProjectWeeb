using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class User
    {
        public User()
        {
            Level = 1;
        }

        public User(string login, string password, int id,List<Card> cards, HashSet<Deck> decks) : this()
        {
            UserName = login;
            Password = password;
            Id = id;
            Cards = cards;
            Decks = decks;
            Cards.AddRange(cards);
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
