
using NovaWars.Factories.ZergFactories.Extensions;
using NovaWars.Model;
using NovaWars.Model.Zergs;

namespace NovaWars.Factories.ZergFactories
{
    public class ZerglingFactory : ZergFactory
    {
        public override IZerg Create()
        {
            return new Zergling();
        }

        public override IZerg CreateNew(string name, int health, int attack, int range)
        {
            return new Zergling
            {
                Name = name,
                Health = health,
                Attack = attack,
                Range = range
            };
        }
    }
}
