

using NovaWars.Model.Zergs;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Levels.Extensions
{
    public interface IZergInitialiser 
    {
        List<IZerg> Initialise(int level);
    }
}
