using NovaWars.Model.Terrans.Extensions;

namespace NovaWars.Factories.TerranFactories
{
    public abstract class TerranFactory
    {
        public abstract ITerran Create();

        public abstract ITerran CreateNew(string name, int health, int attack, int range);
    }
}
