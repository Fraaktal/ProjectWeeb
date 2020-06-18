using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Business
{
    public class CardPosition
    {
        public CardPosition(int pos)
        {
            Position = pos;
            Life = -1;
        }

        public Card Card { get; set; }

        public int Life { get; set; }

        public int Position { get; set; }
    }
}
