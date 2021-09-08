using NovaWars.Factories.NovaFactories;
using NovaWars.Factories.TerranFactories;
using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class TerranOperator : ITerranOperator
    {
        //For each zerg unit deal demage to a random terran unit
        //TODO: Implement Range
        private TerranFactory terranFactory;

        public List<ITerran> ShootTerran(List<ITerran> terrans, List<IZerg> zergs)
        {
            foreach (var zerg in zergs)
            {
                Thread.Sleep(250);
                Random rnd = new Random();
                int r = rnd.Next(terrans.Count);
                terrans[r].Health -= zerg.Attack;
                if (terrans[r].Health <=0)
                {
                    terrans.RemoveAt(r);
                }
            }
            return terrans;
        }

        public Terran UpgradeUnit(Terran unit)
        {
            unit.Attack += 5;
            if(unit.Name == "Nova")
            {
                this.terranFactory = new NovaFactory();
                var nova = this.terranFactory.Create();
                return new Terran()
                {
                    Name = nova.Name,
                    Health = nova.Health,
                    Attack = nova.Attack += 5,
                    Range = nova.Range
                };
            }
            if (unit.Name == "Marine")
            {
                this.terranFactory = new MarineFactory();
                var marine = this.terranFactory.Create();
                return new Terran()
                {
                    Name = marine.Name,
                    Health = marine.Health,
                    Attack = marine.Attack += 5,
                    Range = marine.Range
                };
            }

            return unit;
        }
    }
}
