
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
        private string terranPath = Path.Combine(Environment.CurrentDirectory, @"../../../Infrastucture/Game/Save/SaveFiles/TerranArmySave.csv");
        private string levelPath = Path.Combine(Environment.CurrentDirectory, @"../../../Infrastucture/Game/SaveFiles/LevelSave.csv");

        public List<Terran> ReadSavedFile()
        {
            var xml = File.ReadAllText(terranPath);
            var list = XmlUtil.Deserialize(typeof(List<Terran>), xml) as List<Terran>;

            return list;
        }

        public void SaveLevel(int level, List<ITerran> terran)
        {
            using (TextWriter tw = new StreamWriter(terranPath))
            {
                var list = CastTerranArmy(terran);
                var xml = XmlUtil.Serializer(typeof(List<Terran>), list);
                tw.WriteLine(xml);
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
