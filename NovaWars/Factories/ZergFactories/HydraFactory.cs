using NovaWars.Factories.ZergFactories.Extensions;
using NovaWars.Model.Zergs;
namespace NovaWars.Factories.ZergFactories
{
    public class HydraFactory : ZergFactory
    {
        public override IZerg Create()
        {
            return new Hydra();
        }

        public override IZerg CreateNew(string name, int health, int attack, int range)
        {
            return new Hydra
            {
                Name = name,
                Health = health,
                Attack = attack,
                Range = range
            };
        }
    }
}
