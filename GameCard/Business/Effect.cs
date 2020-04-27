using System;

namespace ProjectWeeb.GameCard.Business
{
    public class Effect
    {
        public Effect(string name,string description, int id)
        {
            Name = name;
            Description = description;
            IdAttack = id;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IdAttack { get; set; }
    }
}
