using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public BattleField PreviousBattlefield { get; set; }

        private void InitializeBattleField()
        {
            Player1Side = new HashSet<CardPosition>();
            Player2Side = new HashSet<CardPosition>();

            for (int i = 0; i < 8; i++)
            {
                Player1Side.Add(new CardPosition(i));
                Player2Side.Add(new CardPosition(i));
            }
        }

        public void SetCardForCurrentPlayer(Card card, int pos)
        {
            var position = Player1Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = card;
            }
        }
        
        public void SetCardForEnnemy(Card card, int pos)
        {
            var position = Player2Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = card;
            }
        }

        public void UnsetCardForPlayer1(int pos)
        {
            var position = Player1Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = null;
            }
        }
        
        public void UnsetCardForEnnemy(int pos)
        {
            var position = Player2Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = null;
            }
        }

        public Card GetEnnemyCard(CardPosition cardPosition)
        {
            return Player2Side.FirstOrDefault(p => p.Position == cardPosition.Position)?.Card;
        }

        public Card GetPlayerCard(CardPosition cardPosition)
        {
            return Player1Side.FirstOrDefault(p => p.Position == cardPosition.Position)?.Card;
        }

        public List<CardPosition> GetEnnemyCardColumn(CardPosition cardPosition)
        {
            List<CardPosition> result = new List<CardPosition>();
            foreach (var cp in Player2Side)
            {
                if (cp.Position == cardPosition.Position)
                {
                    result.Add(cp);
                }
            }

            return result;
        }

        public List<CardPosition> GetHalfEnnemies()
        {
            List<CardPosition> result = new List<CardPosition>();

            for (int i = 0; i < Player2Side.Count; i += 2)
            {
                result.Add(Player2Side.ElementAt(i));
            }

            return result;
        }

        public Card GetEnnemyCardByPosition(int pos)
        {
            return Player2Side.FirstOrDefault(p => p.Position == pos)?.Card;
        }

        public int[] ComputePlayer1Side()
        {
            List<int> res = new List<int>();
            foreach (var cardPosition in Player1Side)
            {
                res.Add(cardPosition.Card?.CardId ?? -1);
            }

            return res.ToArray();
        }
        
        public int[] ComputePlayer2Side()
        {
            List<int> res = new List<int>();
            foreach (var cardPosition in Player2Side)
            {
                res.Add(cardPosition.Card?.CardId ?? -1);
            }

            return res.ToArray();
        }
    }
}
