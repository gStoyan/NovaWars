﻿
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NovaWars.Infrastructure.Game
{
    public interface IGameplayOperator
    {
        void CheckGameOver(int terranCount, int zergCount);

        void Save(int level, ObservableCollection<ITerran> terrans);

        Tuple<List<IZerg>,string> CreateShotZerg(object param, List<IZerg> zergs);

        List<ITerran> EndRound(List<ITerran> terrans, List<IZerg> zergs);

    }
}
