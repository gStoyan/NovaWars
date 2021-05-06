
using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;

namespace NovaWars.Factories.TerranFactories
{
    public class MarineFactory : TerranFactory
    {
        public override ITerran Create()
            => new Marine();

        public override ITerran CreateNew(string name, int health, int attack, int range)
            => new Marine { Name = name, Health = health, Attack = attack, Range = range };
    }
}
