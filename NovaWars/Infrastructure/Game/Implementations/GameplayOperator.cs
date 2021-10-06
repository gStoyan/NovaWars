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
using System.Threading.Tasks;
using System.Windows;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class GameplayOperator : IGameplayOperator
    {
        private IZergOperator zergOperator;
        private ITerranOperator terranOperator;
        private ISaver saver;
        private IConsoleLogger consoleLogger;        
        public GameplayOperator(
            IZergOperator zergOperator,
            ITerranOperator terranOperator,
            ISaver saver,
            IConsoleLogger consoleLogger)
        {
            this.zergOperator = zergOperator;
            this.terranOperator = terranOperator;
            this.saver = saver;
            this.consoleLogger = consoleLogger;

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
        public Tuple<List<IZerg>, string> CreateShotZergAndLog(object param, List<IZerg> zergs) 
        {
            var tAttack = int.Parse(param.ToString().Split('-')[0]);
            var tName = param.ToString().Split('-')[1];
            var newZerg = this.zergOperator.CreateNewZerg(param.ToString());
            zergs = UpdateZergArmy(zergs, newZerg, tAttack);

            var consoleLog = this.consoleLogger.ShootLog(tName, tAttack, newZerg.Name, newZerg.Health);

            return Tuple.Create(zergs, consoleLog);
        }

        public  Tuple<List<ITerran>, List<string>> ZergTurn(List<ITerran> terrans, List<IZerg> zergs)
        {
            List<string> consoleLogs = new List<string>();
            foreach (var zerg in zergs)
            {
                Random rnd = new Random();
                int r = rnd.Next(terrans.Count);
                terrans[r].Health -= zerg.Attack;
                if (terrans[r].Health <= 0)
                {
                    consoleLogs.Add(consoleLogger.ZergKilledLog(terrans[r].Name, terrans[r].Health, zerg.Name));
                    terrans.RemoveAt(r);
                    continue;
                }
                consoleLogs.Add(consoleLogger.ZergAttackLog(terrans[r].Name, terrans[r].Health, zerg.Name, zerg.Attack));
            }
            return Tuple.Create(terrans, consoleLogs);
        }


        private List<IZerg> UpdateZergArmy(List<IZerg> zergs, IZerg newZerg, int tAttack)
        {
            var oldZerg = zergs.Where(z => z.Health == newZerg.Health).FirstOrDefault();
            zergs.Remove(oldZerg);
            newZerg.Health -= tAttack;
            if (newZerg.Health > 0)
            {
                zergs.Insert(0, newZerg);
            }
            return zergs;
        }


        public void Save(int level, ObservableCollection<ITerran> terrans)
        {
            this.saver.SaveLevel(level, terrans.ToList());
        }
    }
}
