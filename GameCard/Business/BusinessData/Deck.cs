using System.Collections.Generic;

namespace ProjectWeeb.GameCard.Business.BusinessData
{
    public class Deck
    {
        public Deck()
        {

        }

        public Deck(List<Card> cards, string name, int idUser)
        {
            Cards = cards;
            Name = name;
            IdUser = idUser;
        }
        
        public int Id { get; set; }

        public int IdUser { get; set; }

        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}
