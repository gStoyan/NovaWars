
using NovaWars.Model.Terrans.Extensions;
using System.Collections.Generic;

namespace NovaWars.Utilities.Save
{
    public interface ISaver
    {
        public void SaveLevel(int level, List<ITerran> terran);
    }
}
