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
            Hp = 20;
            DrawPile = deck.Cards;
            CurrentHand = new List<Card>();
            IsConnected = false;
        }

        public string Pseudo { get; set; }

        public int Level { get; set; }

        public int IdUser { get; set; }

        public int Hp { get; set; }
        
        public List<Card> CurrentHand { get; set; }

        public List<Card> DrawPile { get; set; }

        public string ConnectionId { get; set; }

        public int RemainingActions { get; set; }

        public bool IsConnected { get; set; }
    }
}
