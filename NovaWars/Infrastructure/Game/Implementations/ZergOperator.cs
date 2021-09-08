
using NovaWars.Factories.ZergFactories;
using NovaWars.Model.Zergs;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class ZergOperator : IZergOperator
    {
        public string tName;
        public int tAttack;
        public int tRange;
        public string zName;
        public int zHealth;
        public int zAttack;
        public int zRange;
        public ZerglingFactory zerglingFactory;

        public ZergOperator(string text)
        {
            this.zerglingFactory = new ZerglingFactory();
            this.ParseParams(text);
        }
        public IZerg CreateNewZerg() => this.zerglingFactory.CreateNew(this.zName, this.zHealth, this.zAttack, this.zRange);

        //the text that is recieved is tdamage-tname-trange-zname-zhealth-zattack-zrange
        private void ParseParams(string text)
        {
            var splitText = text.Split('-');
            this.tAttack = int.Parse(splitText[0]);
            this.tName = splitText[1];
            this.tRange = int.Parse(splitText[2]);
            this.zName = splitText[3];
            this.zHealth = int.Parse(splitText[4]);
            this.zAttack = int.Parse(splitText[5]);
            this.zRange = int.Parse(splitText[6]);
        }
    }
}
