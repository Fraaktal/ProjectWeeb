using System.Collections.Generic;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Business
{
    public class Player
    {
        public Player(string pseudo, int level, int idUser, Deck deck)
        {
            Pseudo = pseudo;
            Level = level;
            IdUser = idUser;
            SelectedDeck = deck;
            DrawPile = new HashSet<Card>(); 
            CurrentHand = new HashSet<Card>();
        }

        public string Pseudo { get; set; }

        public int Level { get; set; }

        public int IdUser { get; set; }
        
        public Deck SelectedDeck { get; set; }

        public int Hp { get; set; }
        
        public HashSet<Card> CurrentHand { get; set; }

        public HashSet<Card> DrawPile { get; set; }

    }
}
