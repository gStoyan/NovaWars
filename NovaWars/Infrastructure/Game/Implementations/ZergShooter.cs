
using NovaWars.Factories.ZergFactories;
using NovaWars.Model.Zergs;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class ZergShooter : IZergShooter
    {
        private string name;
        private int health;
        private int attack;
        private int range;
        private ZerglingFactory zerglingFactory;

        public ZergShooter(string text)
        {
            this.zerglingFactory = new ZerglingFactory();
            this.SplitString(text);
        }
        public IZerg CreateNewZerg() => this.zerglingFactory.CreateNew(this.name, this.health, this.attack, this.range);


        //the text that is recieved is damage-name-health-atack-range
        public void SplitString(string text)
        {
            var zergStats = text.Split("-");
            this.name = zergStats[1];
            this.health = int.Parse(zergStats[2]);
            this.attack = int.Parse(zergStats[3]);
            this.range = int.Parse(zergStats[4]);

        }
    }
}
