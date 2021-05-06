
using NovaWars.Model.Zergs;

namespace NovaWars.Model
{
    public class Zergling : IZerg
    {
        private string name;
        private int health;
        private int attack;
        private int range;

        public Zergling()
        {
            name = "Zergling";
            health = 20;
            attack = 3;
            range = 1;
        }
        public string Name { get => name; set { name = value; } }

        public int Health { get => health; set { health = value; } }

        public int Attack { get => attack; set { attack = value; } }

        public int Range { get => range; set { range = value; } }
    }
}
