using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class AttackManager
    {
        public AttackManager(BattleField battleField)
        {
            BattleField = battleField;
            AttackById = InitializeAttackById();
        }

        private BattleField BattleField {get;set;}

        public Dictionary<int, Action<int, int, bool>> AttackById { get; set; }

        public Dictionary<int, Action<int, int, bool>> InitializeAttackById()
        {
            Dictionary<int, Action<int, int, bool>> result =new Dictionary<int, Action<int, int, bool>>
            {
                {0, BasicAttack},
                {1, Megumin_Explosioooon},
                {2, SeeleAttack},
                {3, Joker_All_Out_Attack},
                {4, IronMan_Rayon_laser},
                {5, IronMan_And_I_am_Iron_Man},
                {6, IronMan_Bleeding_Edge_Armor},
                {7, SaikiKusuo_Retour_arriere},
                {8, Kira_Death_note},
                {9, Guts_Berserk},
                {10, AllMight_La_cavalerie_est_la},
                {11, Monika_Sayonara},
                {12, Sans_Bone},
                {13, Pikachu_Pika_pika},
                {14, Pikachu_Tonerre},
                {15, Sonic_Attaque_rapide},
                {16, Zelda_Princesse_Hyrule},
                {17, Mustang_Snap},
                {18, Asuna_Protecc},
                {19, Shiro_Puissance_random},
                {20, Aqua_Inutile},
                {21, Aqua_Reveil_des_dieux},
                {22, Yuno_Genocide},
                {23, Mikasa_Deplacement_Aerien},
                {24, Makise_Time_travel},
            };



            return result;
        }

        private void BasicAttack(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                Card c = BattleField.GetPlayer1CardPositionByPosition(origin).Card;

                if (c != null)
                {
                    BattleField.GetPlayer2CardPositionByPosition(target).Life -= c.Strength;
                }
            }
            else
            {
                Card c = BattleField.GetPlayer2CardPositionByPosition(origin).Card;

                if (c != null)
                {
                    BattleField.GetPlayer1CardPositionByPosition(target).Life -= c.Strength;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void Megumin_Explosioooon(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.Player2Side.Clear();

                Card c = BattleField.GetPlayer1CardPositionByPosition(origin).Card;
                if (c != null)
                {
                    c.CurrentStatus = Card.Status.Sleeping;
                }
            }
            else
            {
                BattleField.Player1Side.Clear();
                
                Card c = BattleField.GetPlayer2CardPositionByPosition(origin).Card;
                if (c != null)
                {
                    c.CurrentStatus = Card.Status.Sleeping;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void SeeleAttack(int origin, int target, bool isPlayer2Targeted)
        {

            for (int i = 0; i < 2; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }

            BattleField.CleanDeadCard();
        }

        private void Joker_All_Out_Attack(int origin, int target, bool isPlayer2Targeted)
        {
            foreach (var cardPosition in BattleField.Player1Side)
            {
                BasicAttack(cardPosition.Position, target, isPlayer2Targeted);
            }
        }

        private void IronMan_Rayon_laser(int origin, int target, bool isPlayer2Targeted)
        {
            for(var i = 0;i<3;i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }
            
            BattleField.CleanDeadCard();
        }

        private void IronMan_And_I_am_Iron_Man(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                List<CardPosition> cards = BattleField.GetHalfPlayer2();

                foreach (var card in cards)
                {
                    BattleField.UnsetCardForPlayer2(card.Position);
                }
            }
            else
            {
                List<CardPosition> cards = BattleField.GetHalfPlayer1();

                foreach (var card in cards)
                {
                    BattleField.UnsetCardForPlayer2(card.Position);
                }
            }
            
            BattleField.CleanDeadCard();
        }

        private void IronMan_Bleeding_Edge_Armor(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                Card c = BattleField.GetPlayer2CardPositionByPosition(target).Card;
                if (c != null)
                {
                    c.Shield = 5;
                }
            }
            else
            {
                Card c = BattleField.GetPlayer1CardPositionByPosition(target).Card;
                if (c != null)
                {
                    c.Shield = 5;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void SaikiKusuo_Retour_arriere(int origin, int target, bool isPlayer2Targeted)
        {
            //Card card = BattleField.PreviousBattlefield.GetPlayerCard(arg2);
            //BattleField.SetCardForPlayer1(card, arg2.Position);
            //BattleField.CleanDeadCard();
        }

        private void Kira_Death_note(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.UnsetCardForPlayer2(target);
            }
            else
            {
                BattleField.UnsetCardForPlayer1(target);
            }

            BattleField.CleanDeadCard();
        }

        private void Guts_Berserk(int origin, int target, bool isPlayer2Targeted)
        {
            BasicAttack(origin, target, isPlayer2Targeted);
            if (target > 0)
            {
                BasicAttack(origin, target - 1, isPlayer2Targeted);
            }

            if (target < 7)
            {
                BasicAttack(origin, target + 1, isPlayer2Targeted);
            }

            BattleField.CleanDeadCard();
        }

        private void AllMight_La_cavalerie_est_la(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Monika_Sayonara(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.UnsetCardForPlayer2(target);
            }
            else
            {
                BattleField.UnsetCardForPlayer1(target);
            }

            BattleField.CleanDeadCard();
        }

        private void Sans_Bone(int origin, int target, bool isPlayer2Targeted)
        {
            Random rand = new Random();
            int attaques = (int) rand.NextDouble() * 3;
            for (int i = 0; i < attaques; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }

            BattleField.CleanDeadCard();
        }

        private void Pikachu_Pika_pika(int origin, int target, bool isPlayer2Targeted)
        {
            if(isPlayer2Targeted)
            {
                Card c = BattleField.GetPlayer2CardPositionByPosition(target).Card;
                if (c != null)
                {
                    c.CurrentStatus = Card.Status.Confuse;
                }
            }
            else
            {
                Card c = BattleField.GetPlayer1CardPositionByPosition(target).Card;
                if (c != null)
                {
                    c.CurrentStatus = Card.Status.Confuse;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void Pikachu_Tonerre(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                Card c = BattleField.GetPlayer1CardPositionByPosition(target).Card;
                if (c != null)
                {
                    BattleField.GetPlayer2CardPositionByPosition(target).Life -= c.Strength + 3;
                }
                 
            }
            else
            {
                Card c = BattleField.GetPlayer2CardPositionByPosition(origin).Card;
                if (c != null)
                {
                    BattleField.GetPlayer1CardPositionByPosition(target).Life -= c.Strength + 3;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void Sonic_Attaque_rapide(int origin, int target, bool isPlayer2Targeted)
        {
            for (int i = 0; i < 3; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }

            BattleField.CleanDeadCard();
        }

        private void Zelda_Princesse_Hyrule(int origin, int target, bool isPlayer2Targeted)
        {
            foreach (var cardPosition in BattleField.Player1Side)
            {
                if (cardPosition.Card != null)
                {
                    cardPosition.Life += 1;
                }
            }

            BattleField.CleanDeadCard();
        }

        private void Mustang_Snap(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Asuna_Protecc(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Shiro_Puissance_random(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                Random rand = new Random();
                int power = (int)rand.NextDouble() * 6 + 1;
                BattleField.GetPlayer2CardPositionByPosition(target).Life -= power;
            }
            else
            {
                Random rand = new Random();
                int power = (int)rand.NextDouble() * 6 + 1;
                BattleField.GetPlayer1CardPositionByPosition(target).Life -= power;
            }

            BattleField.CleanDeadCard();
        }

        private void Aqua_Inutile(int origin, int target, bool isPlayer2Targeted)
        {
            //Je suis un commentaire
        }

        private void Aqua_Reveil_des_dieux(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Yuno_Genocide(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Mikasa_Deplacement_Aerien(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }

        private void Makise_Time_travel(int origin, int target, bool isPlayer2Targeted)
        {
            //BattleField.CleanDeadCard();
            throw new NotImplementedException();
        }
    }
}
