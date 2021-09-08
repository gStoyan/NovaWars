using NovaWars.Model.Terrans.Extensions;
using System;
using System.Runtime.Serialization;

namespace NovaWars.Model.Terrans
{
    [Serializable]
    public class Terran : ITerran
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int Attack { get; set; }

        public int Range { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
