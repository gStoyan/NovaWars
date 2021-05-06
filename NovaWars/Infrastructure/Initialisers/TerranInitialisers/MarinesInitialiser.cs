

using NovaWars.Factories.TerranFactories;
using NovaWars.Infrastructure.Initialisers.Extensions;
using NovaWars.Infrastructure.Levels.Extensions;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Initialisers.TerranInitialisers
{
    public class MarinesInitialiser : ITerranInitialiser
    {
        private int level1 = 2;
        private MarineFactory marineFactory;

        public MarinesInitialiser()
        {
            this.marineFactory = new MarineFactory();
        }
        public List<ITerran> Initialise(int level)
        {
            var marines = new List<ITerran>();
            switch (level)
            {
                case 1:
                    for (int i = 0; i < level1; i++)
                    {
                        marines.Add(this.marineFactory.Create());
                    }
                    return marines;
                default:
                    throw new ArgumentException("Level is out of range");
            }
        }
    }
}
