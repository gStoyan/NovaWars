using NovaWars.Infrastructure.Game.Save;
using NovaWars.Infrastructure.Game.Save.Implementations;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.Utilities.Console;
using NovaWars.Utilities.Console.Implementations;
using NovaWars.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class GameplayOperator : IGameplayOperator
    {
        private IZergOperator zergOperator;
        private ITerranOperator terranShooter;
        private ISaver saver;
        private IConsoleLogger consoleLogger;
        
        public GameplayOperator()
        {
            this.saver = new Saver();
            this.terranShooter = new TerranOperator();
            this.consoleLogger = new ConsoleLogger();

        }
        public void CheckGameOver(int terranCount, int zergCount)
        {            
            if (zergCount == 0)
            {
                IntermissionWindow intermissionWindow = new IntermissionWindow();
                intermissionWindow.Show();
            }
            if (terranCount == 0)
            {
                MessageBox.Show("You Lost!");
            }
        }

        public Tuple<List<IZerg>,string> CreateShotZerg(object param, List<IZerg> zergs) 
        {
            var tAttack = int.Parse(param.ToString().Split('-')[0]);
            var tName = param.ToString().Split('-')[1];
            this.zergOperator = new ZergOperator(param.ToString());
            var newZerg = this.zergOperator.CreateNewZerg();

            var oldZerg = zergs.Where(z => z.Health == newZerg.Health).FirstOrDefault();
            newZerg.Health -= tAttack;
            zergs.Remove(oldZerg);
            if (newZerg.Health > 0)
            {
                zergs.Insert(0, newZerg);
            }
            var consoleLog = this.consoleLogger.ShootLog(tName, tAttack, newZerg.Name, newZerg.Health);

            return Tuple.Create(zergs, consoleLog);
        } 

        public List<ITerran> EndRound(List<ITerran> terrans, List<IZerg> zergs) =>
            this.terranShooter.ShootTerran(terrans, zergs);

        public void Save(int level, ObservableCollection<ITerran> terrans)
        {
            this.saver.SaveLevel(level, terrans.ToList());
        }

    }
}
