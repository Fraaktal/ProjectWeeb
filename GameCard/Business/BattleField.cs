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

        public HashSet<CardPosition> CurrentPlayerSide { get; set; }

        public HashSet<CardPosition> EnnemySide { get; set; }

        public BattleField PreviousBattlefield { get; set; }

        private void InitializeBattleField()
        {
            CurrentPlayerSide = new HashSet<CardPosition>();
            EnnemySide = new HashSet<CardPosition>();

            for (int i = 0; i < 4; i++)
            {
                CurrentPlayerSide.Add(new CardPosition(i, 1));
                EnnemySide.Add(new CardPosition(i, 1));
            }

            for (int i = 0; i < 6; i++)
            {
                CurrentPlayerSide.Add(new CardPosition(i, 1));
                EnnemySide.Add(new CardPosition(i, 1));
            }
        }

        public void SetCardForCurrentPlayer(Card card, int x, int y)
        {
            var position = CurrentPlayerSide.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = card;
            }
        }
        
        public void SetCardForEnnemy(Card card, int x, int y)
        {
            var position = EnnemySide.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = card;
            }
        }

        public void UnsetCardForPlayer1(int x, int y)
        {
            var position = CurrentPlayerSide.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = null;
            }
        }
        
        public void UnsetCardForEnnemy(int x, int y)
        {
            var position = EnnemySide.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position != null)
            {
                position.Card = null;
            }
        }

        public Card GetEnnemyCard(CardPosition cardPosition)
        {
            return EnnemySide.FirstOrDefault(cp => cp.X == cardPosition.X && cp.Y == cardPosition.Y)?.Card;
        }

        public Card GetPlayerCard(CardPosition cardPosition)
        {
            return CurrentPlayerSide.FirstOrDefault(cp => cp.X == cardPosition.X && cp.Y == cardPosition.Y)?.Card;
        }

        public List<CardPosition> GetEnnemyCardColumn(CardPosition cardPosition)
        {
            List<CardPosition> result = new List<CardPosition>();
            foreach (var cp in EnnemySide)
            {
                if (cp.X == cardPosition.X)
                {
                    result.Add(cp);
                }
            }

            return result;
        }

        public List<CardPosition> GetHalfEnnemies()
        {
            List<CardPosition> result = new List<CardPosition>();

            for (int i = 0; i < EnnemySide.Count; i += 2)
            {
                result.Add(EnnemySide.ElementAt(i));
            }

            return result;
        }

        public Card GetEnnemyCardByPosition(int x, int y)
        {
            return EnnemySide.FirstOrDefault(cp => cp.X == x && cp.Y == y)?.Card;
        }
    }
}
