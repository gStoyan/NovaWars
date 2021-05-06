

using NovaWars.Model.Terrans.Extensions;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Initialisers.Extensions
{
    public interface ITerranInitialiser
    {
       List<ITerran> Initialise(int level);
    }
}
