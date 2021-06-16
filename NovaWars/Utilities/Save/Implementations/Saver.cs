
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Utilities.XML;
using System;
using System.Collections.Generic;
using System.IO;

namespace NovaWars.Utilities.Save.Implementations
{
    public class Saver : ISaver
    {
        private string terranPath = Path.Combine(Environment.CurrentDirectory, @"../../../Utilities/Save/SaveFiles/TerranArmySave.csv");
        private string levelPath = Path.Combine(Environment.CurrentDirectory, @"../../../Utilities/Save/SaveFiles/LevelSave.csv");
        
        public void SaveLevel(int level, List<ITerran> terran)
        {
            using (TextWriter tw = new StreamWriter(terranPath))
            {
                var xml = XmlUtil.Serializer(typeof(List<ITerran>), terran);
                tw.WriteLine(xml);
            }
        }
    }
}
