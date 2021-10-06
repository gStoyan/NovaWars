using NovaWars.Factories.ZergFactories;
using NovaWars.Infrastructure.Levels.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Levels.ZergInitialisers
{
    public class HydrasInitialiser : IZergInitialiser
    {
        private int level1 = 0;

        private HydraFactory hydraFactory;

        public HydrasInitialiser()
        {
            this.hydraFactory = new HydraFactory();
        }
        public List<IZerg> Initialise(int level)
        {
            var hydras = new List<IZerg>();
            switch (level)
            {
                case 1:
                    for (int i = 0; i < level1; i++)
                    {
                        hydras.Add(this.hydraFactory.Create());
                    }
                    return hydras;
                default:
                    throw new ArgumentException("Level is out of range");
            }
        }
    }
}
