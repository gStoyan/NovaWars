
using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game.Save
{
    public interface ISaver
    {
        public void SaveLevel(int level, List<ITerran> terran);

        public List<Terran> ReadTerranSavedFile();

        public int ReadLevelSavedFile();
    }
}
