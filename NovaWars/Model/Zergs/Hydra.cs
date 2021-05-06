using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaWars.Model.Zergs
{
    public class Hydra : IZerg
    {
        private string name;
        private int health;
        private int attack;
        private int range;

        public Hydra()
        {
            name = "Hydra";
            health = 40;
            attack = 8;
            range = 6;
        }
        public string Name { get => name; set { name = value; } }
        public int Health { get => health; set { health = value; } }
        public int Attack { get => attack; set { attack = value; } }
        public int Range { get => range; set { range = value; } }
    }
}
