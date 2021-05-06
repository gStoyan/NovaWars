
using NovaWars.Factories.NovaFactories;
using NovaWars.Factories.ZergFactories;
using NovaWars.Infrastructure.Initialisers.Extensions;
using NovaWars.Infrastructure.Levels.Extensions;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NovaWars.Infrastructure.Levels
{
    public class Initialiser
    {
        private NovaFactory novaFactory;

        public Initialiser()
        {
            this.novaFactory = new NovaFactory();
        }


        public Dictionary<List<ITerran>, List<IZerg>> InitialiseLevel(int level)
        {
            var result = new Dictionary<List<ITerran>, List<IZerg>>();
            var zergList = new List<IZerg>();
            var terranList = new List<ITerran>();

            foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
               .Where(mytype => mytype.GetInterfaces().Contains(typeof(IZergInitialiser))))
            {
                var instance = Activator.CreateInstance(mytype);
                MethodInfo method = mytype.GetMethod("Initialise", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                zergList.AddRange((List<IZerg>)method.Invoke(instance, new object[] { level }));
            }

            foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
              .Where(mytype => mytype.GetInterfaces().Contains(typeof(ITerranInitialiser))))
            {
                var instance = Activator.CreateInstance(mytype);
                MethodInfo method = mytype.GetMethod("Initialise", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                terranList.AddRange((List<ITerran>)method.Invoke(instance, new object[] { level }));
            }

            result.Add(terranList, zergList);
            return result;
        }
    }
}
