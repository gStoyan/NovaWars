using NovaWars.Model.Zergs;
namespace NovaWars.Factories.ZergFactories.Extensions
{
    public abstract class ZergFactory
    {
        public abstract IZerg Create();

        public abstract IZerg CreateNew(string name, int health, int attack, int range);
    }
}
