using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Business
{
    public class CardPosition
    {
        public CardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Card Card { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
