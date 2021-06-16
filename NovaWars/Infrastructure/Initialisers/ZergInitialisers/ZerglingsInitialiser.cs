using NovaWars.Factories.ZergFactories;
using NovaWars.Infrastructure.Levels.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Levels.ZergInitialisers
{
    public class ZerglingsInitialiser : IZergInitialiser
    {
        private int level1 = 1;

        private ZerglingFactory zerglingFactory;

        public ZerglingsInitialiser()
        {
            this.zerglingFactory = new ZerglingFactory();
        }


        public List<IZerg> Initialise(int level)
        {
            var zerglings = new List<IZerg>();
            switch (level)
            {
                case 1:
                    for (int i = 0; i < level1; i++)
                    {
                        zerglings.Add(this.zerglingFactory.Create());
                    }
                    return zerglings;
                default:
                    throw new ArgumentException("Level is out of range");
            }
        }
    }
}
