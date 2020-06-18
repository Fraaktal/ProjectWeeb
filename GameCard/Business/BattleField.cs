using System;
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
                position.Life = card.Life;
            }
        }
        
        public void SetCardForPlayer2(Card card, int pos)
        {
            var position = Player2Side.FirstOrDefault(p => p.Position == pos);
            if (position != null)
            {
                position.Card = card;
                position.Life = card.Life;
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
                int life = cardPosition.Life;
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
                int life = cardPosition.Life;
                int st = cardPosition.Card?.Strength ?? -1;
                res.Add(new[] { id, life, st });
            }

            return res.ToArray();
        }

        public CardPosition GetPlayer1CardPositionByPosition(in int positionOrigin)
        {
            return Player1Side.ElementAtOrDefault(positionOrigin);
        }

        public CardPosition GetPlayer2CardPositionByPosition(in int positionOrigin)
        {
            return Player2Side.ElementAtOrDefault(positionOrigin);
        }

        public void CleanDeadCard()
        {
            foreach (var cardPosition in Player1Side)
            {
                if (cardPosition.Card != null && cardPosition.Life < 1)
                {
                    UnsetCardForPlayer1(cardPosition.Position);
                }
            }

            foreach (var cardPosition in Player2Side)
            {
                if (cardPosition.Card != null && cardPosition.Life < 1)
                {
                    UnsetCardForPlayer2(cardPosition.Position);
                }
            }
        }

        public int GetPlayer2CardCount()
        {
            int res = 0;
            foreach (var cardPosition in Player2Side)
            {
                if (cardPosition.Card != null)
                {
                    res++;
                }
            }

            return res;
        }

        public int GetPlayer1CardCount()
        {
            int res = 0;
            foreach (var cardPosition in Player1Side)
            {
                if (cardPosition.Card != null)
                {
                    res++;
                }
            }

            return res;
        }
    }
}
