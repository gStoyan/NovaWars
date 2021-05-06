
using NovaWars.Factories.NovaFactories;
using NovaWars.Infrastructure.Initialisers.Extensions;
using NovaWars.Model.Terrans.Extensions;
using System;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Initialisers.TerranInitialisers
{
    public class NovaInitialiser : ITerranInitialiser
    {
        private int level1 = 1;
        private NovaFactory novaFactory;

        public NovaInitialiser()
        {
            this.novaFactory = new NovaFactory();
        }
        public List<ITerran> Initialise(int level)
        {
            var nova = new List<ITerran>();
            switch (level)
            {
                case 1:
                    for (int i = 0; i < level1; i++)
                    {
                        nova.Add(this.novaFactory.Create());
                    }
                    return nova;
                default:
                    throw new ArgumentException("Level is out of range");
            }
        }
    }
}
