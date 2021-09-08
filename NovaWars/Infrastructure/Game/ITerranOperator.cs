using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game
{
    public interface ITerranOperator
    {
        List<ITerran> ShootTerran(List<ITerran> terrans, List<IZerg> zergs);

        Terran UpgradeUnit(Terran unit);
        
    }
}
