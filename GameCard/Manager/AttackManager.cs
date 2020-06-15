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
            AttackById = initializeAttackById();
        }

        private BattleField BattleField {get;set;}

        public Dictionary<int, Action<int, int, bool>> AttackById { get; set; }

        public Dictionary<int, Action<int, int, bool>> initializeAttackById()
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
                BattleField.GetPlayer2CardByPosition(target).Life -= BattleField.GetPlayer1CardByPosition(origin).Strength;
            }
            else
            {
                BattleField.GetPlayer2CardByPosition(target).Life -= BattleField.GetPlayer1CardByPosition(origin).Strength;
            }
        }

        private void Megumin_Explosioooon(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.Player2Side.Clear();
                BattleField.GetPlayer1CardByPosition(origin).CurrentStatus = Card.Status.Sleeping;
            }
            else
            {
                BattleField.Player1Side.Clear();
                BattleField.GetPlayer1CardByPosition(origin).CurrentStatus = Card.Status.Sleeping;
            }
        }

        private void SeeleAttack(int origin, int target, bool isPlayer2Targeted)
        {

            for (int i = 0; i < 2; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }
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
            
        }

        private void IronMan_Bleeding_Edge_Armor(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.GetPlayer2CardByPosition(target).Shield = 5;
            }
            else
            {
                BattleField.GetPlayer1CardByPosition(target).Shield = 5;
            }
        }

        private void SaikiKusuo_Retour_arriere(int origin, int target, bool isPlayer2Targeted)
        {
            //Card card = BattleField.PreviousBattlefield.GetPlayerCard(arg2);
            //BattleField.SetCardForPlayer1(card, arg2.Position);
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
        }

        private void AllMight_La_cavalerie_est_la(int origin, int target, bool isPlayer2Targeted)
        {
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
        }

        private void Sans_Bone(int origin, int target, bool isPlayer2Targeted)
        {
            Random rand = new Random();
            int attaques = (int) rand.NextDouble() * 3;
            for (int i = 0; i < attaques; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }
        }

        private void Pikachu_Pika_pika(int origin, int target, bool isPlayer2Targeted)
        {
            if(isPlayer2Targeted)
            {
                BattleField.GetPlayer2CardByPosition(target).CurrentStatus = Card.Status.Confuse;
            }
            else
            {
                BattleField.GetPlayer1CardByPosition(target).CurrentStatus = Card.Status.Confuse;
            }
        }

        private void Pikachu_Tonerre(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                BattleField.GetPlayer2CardByPosition(target).Life -= BattleField.GetPlayer1CardByPosition(origin).Strength + 3;
            }
            else
            {
                BattleField.GetPlayer1CardByPosition(target).Life -= BattleField.GetPlayer2CardByPosition(origin).Strength + 3;
            }
        }

        private void Sonic_Attaque_rapide(int origin, int target, bool isPlayer2Targeted)
        {
            for (int i = 0; i < 3; i++)
            {
                BasicAttack(origin, target, isPlayer2Targeted);
            }
        }

        private void Zelda_Princesse_Hyrule(int origin, int target, bool isPlayer2Targeted)
        {
            foreach (var cardPosition in BattleField.Player1Side)
            {
                cardPosition.Card.Life += 1;
            }
        }

        private void Mustang_Snap(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }

        private void Asuna_Protecc(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }

        private void Shiro_Puissance_random(int origin, int target, bool isPlayer2Targeted)
        {
            if (isPlayer2Targeted)
            {
                Random rand = new Random();
                int power = (int)rand.NextDouble() * 6 + 1;
                BattleField.GetPlayer2CardByPosition(target).Life -= power;
            }
            else
            {
                Random rand = new Random();
                int power = (int)rand.NextDouble() * 6 + 1;
                BattleField.GetPlayer1CardByPosition(target).Life -= power;
            }
        }

        private void Aqua_Inutile(int origin, int target, bool isPlayer2Targeted)
        {
            //Je suis un commentaire
        }

        private void Aqua_Reveil_des_dieux(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }

        private void Yuno_Genocide(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }

        private void Mikasa_Deplacement_Aerien(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }

        private void Makise_Time_travel(int origin, int target, bool isPlayer2Targeted)
        {
            throw new NotImplementedException();
        }
    }
}
