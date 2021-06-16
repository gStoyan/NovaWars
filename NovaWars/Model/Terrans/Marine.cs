using NovaWars.Model.Terrans.Extensions;
using System;
using System.Runtime.Serialization;

namespace NovaWars.Model.Terrans
{
    [Serializable]
    public class Marine :  ITerran 
    {
        private string name;
        private int health;
        private int attack;
        private int range;
        public Marine()
        {
            name = "Marine";
            health = 20;
            attack = 2;
            Range = 3;
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
