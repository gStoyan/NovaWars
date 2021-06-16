
using System.Runtime.Serialization;

namespace NovaWars.Model.Terrans.Extensions
{
    public interface ITerran : ISerializable
    {
        public abstract string Name { get; set; }

        public abstract int Health { get; set; }

        public abstract int Attack { get; set; }

        public abstract int Range { get; set; }
    }
}
