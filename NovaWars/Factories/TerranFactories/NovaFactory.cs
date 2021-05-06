using NovaWars.Factories.TerranFactories;
using NovaWars.Model.Nova;
using NovaWars.Model.Terrans.Extensions;

namespace NovaWars.Factories.NovaFactories
{
    public class NovaFactory : TerranFactory
    {
        public override ITerran Create() 
            => new Nova();

        public override ITerran CreateNew(string name, int health, int attack, int range) 
            => new Nova { Name = name, Health = health, Attack = attack, Range = range };
    }
}
