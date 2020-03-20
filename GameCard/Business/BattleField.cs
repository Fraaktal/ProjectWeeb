using System.Collections.Generic;
using System.Linq;
using ProjectWeeb.GameCard.Business.BusinessData;

namespace ProjectWeeb.GameCard.Business
{
    public class BattleField
    {
        public BattleField()
        {
            InitializeBattleField();
        }

        public HashSet<CardPosition> Player1Side { get; set; }

        public HashSet<CardPosition> Player2Side { get; set; }

        private void InitializeBattleField()
        {
            Player1Side = new HashSet<CardPosition>();
            Player2Side = new HashSet<CardPosition>();

            for (int i = 0; i < 4; i++)
            {
                Player1Side.Add(new CardPosition(i, 1));
                Player2Side.Add(new CardPosition(i, 1));
            }

            for (int i = 0; i < 6; i++)
            {
                Player1Side.Add(new CardPosition(i, 1));
                Player2Side.Add(new CardPosition(i, 1));
            }
        }

        public void SetCardForPlayer1(Card card, int x, int y)
        {
            var position = Player1Side.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = card;
            }
        }
        
        public void SetCardForPlayer2(Card card, int x, int y)
        {
            var position = Player2Side.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = card;
            }
        }

        public void UnsetCardForPlayer1(int x, int y)
        {
            var position = Player1Side.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = null;
            }
        }
        
        public void UnsetCardForPlayer2(int x, int y)
        {
            var position = Player2Side.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = null;
            }
        }
    }
}
