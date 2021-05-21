using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class TerranShooter : ITerranShooter
    {
        //For each zerg unit deal demage to a random terran unit
        //TODO: Implement Range
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
    }
}
