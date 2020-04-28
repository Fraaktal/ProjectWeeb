using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWeeb.GameCard.Business;

namespace ProjectWeeb.GameCard.Manager
{
    public class AttackManager
    {
        public AttackManager()
        {

        }

        //DANS CHACUNE DES ATTAQUE FAIRE APPEL AU SINGLETON POUR MODIFIER L'ETAT DU JEU

        public Dictionary<int, Action<CardPosition, CardPosition>> AttackById { get; set; }
    }
}
