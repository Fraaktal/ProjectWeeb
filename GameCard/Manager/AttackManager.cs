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
        public AttackManager()
        {

        }

        private BattleField BattleField
        {
            get
            {
                return CWebSite.GetInstance().GameManager.GameController.BattleField;
            }
        }

        public Dictionary<int, Action<CardPosition, CardPosition>> AttackById { get; set; }

        public Dictionary<int, Action<CardPosition, CardPosition>> initializeAttackById()
        {
            Dictionary<int, Action<CardPosition, CardPosition>> result =new Dictionary<int, Action<CardPosition, CardPosition>>
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

        private void BasicAttack(CardPosition arg1, CardPosition arg2)
        {
            BattleField.GetEnnemyCard(arg2).Life -= arg1.Stength;
        }

        private void Megumin_Explosioooon(CardPosition arg1, CardPosition arg2)
        {
            BattleField.EnnemySide.Clear();
            BattleField.GetPlayerCard(arg1).CurrentStatus = Card.Status.Sleeping;
        }

        private void SeeleAttack(CardPosition arg1, CardPosition arg2)
        {
            for (int i = 0; i < 2; i++)
            {
                BasicAttack(arg1, arg2);
            }
        }

        private void Joker_All_Out_Attack(CardPosition arg1, CardPosition arg2)
        {
            foreach (var cardPosition in BattleField.CurrentPlayerSide)
            {
                BasicAttack(cardPosition, arg2);
            }
        }

        private void IronMan_Rayon_laser(CardPosition arg1, CardPosition arg2)
        {
            var cards = BattleField.GetEnnemyCardColumn(arg2);

            foreach (var card in cards)
            {
                BasicAttack(arg1,card);
            }
        }

        private void IronMan_And_I_am_Iron_Man(CardPosition arg1, CardPosition arg2)
        {
            List<CardPosition> cards = BattleField.GetHalfEnnemies();

            foreach (var card in cards)
            {
                BattleField.UnsetCardForEnnemy(card.X,card.Y);
            }
        }

        private void IronMan_Bleeding_Edge_Armor(CardPosition arg1, CardPosition arg2)
        {
            BattleField.GetPlayerCard(arg1).Shield = 5;
        }

        private void SaikiKusuo_Retour_arriere(CardPosition arg1, CardPosition arg2)
        {
            Card card = BattleField.PreviousBattlefield.GetPlayerCard(arg2);
            BattleField.SetCardForCurrentPlayer(card, arg2.X,arg2.Y);
        }

        private void Kira_Death_note(CardPosition arg1, CardPosition arg2)
        {
            BattleField.UnsetCardForEnnemy(arg2.X, arg2.Y);
        }

        private void Guts_Berserk(CardPosition arg1, CardPosition arg2)
        {
            BattleField.GetEnnemyCardByPosition(arg2.X-1, arg2.Y).Life -= 1;
            BattleField.GetEnnemyCardByPosition(arg2.X+1, arg2.Y).Life -= 1;
            BattleField.GetEnnemyCard(arg2);
        }

        private void AllMight_La_cavalerie_est_la(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Monika_Sayonara(CardPosition arg1, CardPosition arg2)
        {
            BattleField.UnsetCardForEnnemy(arg2.X, arg2.Y);
        }

        private void Sans_Bone(CardPosition arg1, CardPosition arg2)
        {
            Random rand = new Random();
            int attaques = (int) rand.NextDouble() * 3;
            for (int i = 0; i < attaques; i++)
            {
                BasicAttack(arg1,arg2);
            }
        }

        private void Pikachu_Pika_pika(CardPosition arg1, CardPosition arg2)
        {
            BattleField.GetEnnemyCard(arg2).CurrentStatus = Card.Status.Confuse;
        }

        private void Pikachu_Tonerre(CardPosition arg1, CardPosition arg2)
        {
            BattleField.GetEnnemyCard(arg2).Life -= arg1.Strength + 3;
        }

        private void Sonic_Attaque_rapide(CardPosition arg1, CardPosition arg2)
        {
            for (int i = 0; i < 3; i++)
            {
                BasicAttack(arg1, arg2);
            }
        }

        private void Zelda_Princesse_Hyrule(CardPosition arg1, CardPosition arg2)
        {
            foreach (var cardPosition in BattleField.CurrentPlayerSide)
            {
                cardPosition.Card.Life += 1;
            }
        }

        private void Mustang_Snap(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Asuna_Protecc(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Shiro_Puissance_random(CardPosition arg1, CardPosition arg2)
        {
            Random rand = new Random();
            int power = (int)rand.NextDouble() * 6 + 1;
            BattleField.GetEnnemyCard(arg2).Life -= power;
        }

        private void Aqua_Inutile(CardPosition arg1, CardPosition arg2)
        {
            //Je suis un commentaire
        }

        private void Aqua_Reveil_des_dieux(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Yuno_Genocide(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Mikasa_Deplacement_Aerien(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }

        private void Makise_Time_travel(CardPosition arg1, CardPosition arg2)
        {
            throw new NotImplementedException();
        }
    }
}
