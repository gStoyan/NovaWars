
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System.Collections.Generic;

namespace NovaWars.Infrastructure.Game
{
    public interface IGameplayOperator
    {
        void CheckGameOver(int terranCount, int zergCount);

        IZerg ShootZerg(string param);


        List<ITerran> EndRound(List<ITerran> terrans, List<IZerg> zergs);
    }
}
