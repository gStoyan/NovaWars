using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Utilities.XML;
using System;
using System.Collections.Generic;
using System.IO;

namespace NovaWars.Infrastructure.Game.Save.Implementations
{
    public class Saver : ISaver
    {
        private string terranPath = @"C:\VS19Repos\NovaWars\NovaWars\Infrastructure\Game\Save\SaveFiles\TerranArmySave.csv";
        private string levelPath = @"C:\VS19Repos\NovaWars\NovaWars\Infrastructure\Game\Save\SaveFiles\LevelSave.csv";

        public List<Terran> ReadTerranSavedFile()
        {
            var xml = File.ReadAllText(terranPath);
            var list = XmlUtil.Deserialize(typeof(List<Terran>), xml) as List<Terran>;

            return list;
        }

        public int ReadLevelSavedFile()
        {
            return int.Parse(File.ReadAllText(levelPath));
        }

        public void SaveLevel(int level, List<ITerran> terran)
        {
            using (TextWriter tw = new StreamWriter(terranPath))
            {
                var list = CastTerranArmy(terran);
                var xml = XmlUtil.Serializer(typeof(List<Terran>), list);
                tw.WriteLine(xml);
            }

            using (TextWriter writer = new StreamWriter(levelPath))
            {
                writer.Write(level);
            }
        }

        private List<Terran> CastTerranArmy(List<ITerran> terran)
        {
            var list = new List<Terran>();
            foreach (var t in terran)
            {
                list.Add(new Terran()
                {
                    Name = t.Name,
                    Health = t.Health,
                    Attack = t.Attack,
                    Range = t.Range
                });
            }
            return list;
        }
    }
}
