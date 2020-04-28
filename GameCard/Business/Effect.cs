using System;

namespace ProjectWeeb.GameCard.Business
{
    public class Effect
    {
        public enum TypeOfEffect
        {
            Passive = 0, Active = 1
        }

        public Effect(string name,string description, int id, TypeOfEffect type)
        {
            Name = name;
            Description = description;
            IdAttack = id;
            EffectType = type;
        }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int IdAttack { get; set; }

        public TypeOfEffect EffectType { get; set; }
    }
}