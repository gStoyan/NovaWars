using PropertyChanged;
using System.ComponentModel;

namespace NovaWars.Model.Zergs
{
    public interface IZerg  
    {             
        public abstract string Name { get; set; }
        public abstract int Health { get; set; }
        public abstract int Attack { get; set; }        
        public abstract int Range { get; set; }
    }
}
