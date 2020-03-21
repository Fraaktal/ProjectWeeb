using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Business
{
    public class Player
    {
        public Player()
        {
            DrawPile = new HashSet<Card>(); 
            CurrentHand = new HashSet<Card>();
        }

        public string Pseudo { get; set; }

        public int Level { get; set; }

        public int IdUser { get; set; }

        public List<Deck> Decks { get; set; }

        public Deck SelectedDeck { get; set; }

        public int Hp { get; set; }

        public string ConnectionId { get; set; }

        public HashSet<Card> CurrentHand { get; set; }

        public HashSet<Card> DrawPile { get; set; }

    }
}
