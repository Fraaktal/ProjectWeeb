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

        public List<CardPosition> Player1Side { get; set; }

        public List<CardPosition> Player2Side { get; set; }

        public BattleField PreviousBattlefield { get; set; }

        private void InitializeBattleField()
        {
            Player1Side = new List<CardPosition>();
            Player2Side = new List<CardPosition>();

            for (int i = 0; i < 8; i++)
            {
                Player1Side.Add(new CardPosition(i));
                Player2Side.Add(new CardPosition(i));
            }
        }

        public void SetCardForPlayer1(Card card, int pos)
        {
            var position = Player1Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = card;
            }
        }
        
        public void SetCardForPlayer2(Card card, int pos)
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
        
        public void UnsetCardForPlayer2(int pos)
        {
            var position = Player2Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = null;
            }
        }

        public Card GetPlayer2Card(CardPosition cardPosition)
        {
            return Player2Side.FirstOrDefault(p => p.Position == cardPosition.Position)?.Card;
        }

        public Card GetPlayer1Card(CardPosition cardPosition)
        {
            return Player1Side.FirstOrDefault(p => p.Position == cardPosition.Position)?.Card;
        }

        public List<CardPosition> GetPlayer2CardColumn(CardPosition cardPosition)
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

        public List<CardPosition> GetHalfPlayer2()
        {
            List<CardPosition> result = new List<CardPosition>();

            for (int i = 0; i < Player2Side.Count; i += 2)
            {
                result.Add(Player2Side.ElementAt(i));
            }

            return result;
        }

        public List<CardPosition> GetHalfPlayer1()
        {
            List<CardPosition> result = new List<CardPosition>();

            for (int i = 0; i < Player1Side.Count; i += 2)
            {
                result.Add(Player1Side.ElementAt(i));
            }

            return result;
        }

        public int[][] ComputePlayer1Side()
        {
            List<int[]> res = new List<int[]>();
            foreach (var cardPosition in Player1Side)
            {
                int id = cardPosition.Card?.CardId ?? -1;
                int life = cardPosition.Card?.Life ?? -1;
                int st = cardPosition.Card?.Strength ?? -1;
                res.Add(new []{id,life,st});
            }

            return res.ToArray();
        }
        
        public int[][] ComputePlayer2Side()
        {
            List<int[]> res = new List<int[]>();
            foreach (var cardPosition in Player2Side)
            {
                int id = cardPosition.Card?.CardId ?? -1;
                int life = cardPosition.Card?.Life ?? -1;
                int st = cardPosition.Card?.Strength ?? -1;
                res.Add(new[] { id, life, st });
            }

            return res.ToArray();
        }

        public Card GetPlayer1CardByPosition(in int positionOrigin)
        {
            return Player1Side.ElementAtOrDefault(positionOrigin)?.Card;
        }

        public Card GetPlayer2CardByPosition(in int positionOrigin)
        {
            return Player2Side.ElementAtOrDefault(positionOrigin)?.Card;
        }

        public void DamagePlayer2CardByPosition(in int positionTargeted, in int cardStrength)
        {
            CardPosition cardP = Player2Side.ElementAtOrDefault(positionTargeted);
            if (cardP != null)
            {
                cardP.Card.Life -= cardStrength;

                if (cardP.Card.Life <= 0)
                {
                    UnsetCardForPlayer2(positionTargeted);
                }
            }
        }

        public void DamagePlayer1CardByPosition(in int positionTargeted, in int cardStrength)
        {
            CardPosition cardP = Player1Side.ElementAtOrDefault(positionTargeted);
            if (cardP != null)
            {
                cardP.Card.Life -= cardStrength;

                if (cardP.Card.Life <= 0)
                {
                    UnsetCardForPlayer1(positionTargeted);
                }
            }
        }

      
    }
}
