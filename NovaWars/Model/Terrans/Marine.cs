using NovaWars.Model.Terrans.Extensions;

namespace NovaWars.Model.Terrans
{
    public class Marine : ITerran
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
    }
}
