using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Business
{
    public class Player
    {
        public Player()
        {
            
        }

        public string Pseudo { get; set; }

        public int Level { get; set; }

        public int IdPlayer { get; set; }

        public List<Deck> Decks { get; set; }

        public Deck SelectedDeck { get; set; }

        public int Hp { get; set; }
    }
}
