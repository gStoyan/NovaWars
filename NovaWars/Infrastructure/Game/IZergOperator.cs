using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game
{
    public interface IZergOperator
    {
        IZerg CreateNewZerg();
    }
}
