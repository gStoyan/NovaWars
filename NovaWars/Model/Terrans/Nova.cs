
using NovaWars.Model.Terrans.Extensions;
using System;
using System.Runtime.Serialization;

namespace NovaWars.Model.Nova
{
    [Serializable]
    public class Nova : ITerran
    {
        private string name;
        private int health;
        private int attack;
        private int range;
        public Nova()
        {
            name = "Nova";
            health = 250;
            attack = 25;
            Range = 9;
        }

        public string Name { get => name; set { name = value; } }

        public int Health { get => health; set { health = value; } }

        public int Attack { get => attack; set { attack = value; } }

        public int Range { get => range; set { range = value; } }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
