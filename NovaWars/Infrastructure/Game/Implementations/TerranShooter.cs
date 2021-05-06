

using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class TerranShooter : ITerranShooter
    {
        //For each zerg unit deal demage to a random terran unit
        //TODO: Implement Range
        public Dictionary<List<ITerran>, List<IZerg>> ShootTerran(List<ITerran> terrans, List<IZerg> zergs)
        {
            foreach (var zerg in zergs)
            {
                Random rnd = new Random();
                int r = rnd.Next(terrans.Count);
                terrans[r].Health -= zerg.Attack;
                if (terrans[r].Health <=0)
                {
                    terrans.RemoveAt(r);
                }
            }
            var armies = new Dictionary<List<ITerran>, List<IZerg>>();
            armies.Add(terrans, zergs);
            return armies;
        }
    }
}
