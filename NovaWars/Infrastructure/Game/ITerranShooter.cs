using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game
{
    public interface ITerranShooter
    {
        Dictionary<List<ITerran>, List<IZerg>> ShootTerran(List<ITerran> terrans, List<IZerg> zergs);
    }
}
