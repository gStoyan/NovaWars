
using NovaWars.Infrastructure.Game;
using NovaWars.Infrastructure.Game.Implementations;
using NovaWars.Infrastructure.Levels;
using NovaWars.Model;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.Utilities.Console;
using NovaWars.Utilities.Console.Implementations;
using NovaWars.ViewModel.Base;
using NovaWars.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NovaWars.ViewModel
{
    public class WarsViewModel : BaseViewModel
    {
        public ICommand UpdateCommand { get; set; }
        private int level = 1;
        private RelayCommand<object> myCommand;
        private Initialiser initialiser;
        private IGameplayOperator gameplayOperator;
        private IConsoleLogger consoleLogger;
        private ObservableCollection<IZerg> zergs;
        private ObservableCollection<ITerran> terrans;

        public WarsViewModel()
        {
            this.gameplayOperator = new GameplayOperator();
            this.consoleLogger = new ConsoleLogger();
            this.initialiser = new Initialiser();
            this.zergs = new ObservableCollection<IZerg>(this.initialiser.InitialiseLevel(level).Values.First());
            this.terrans = new ObservableCollection<ITerran>(this.initialiser.InitialiseLevel(level).Keys.First().OrderByDescending(t => t.Attack));
            this.Turns = this.Terrans.Count;
            this.SelectedItem = 0;
        }
        public ObservableCollection<IZerg> Zergs { get => zergs; set { zergs = value; } }

        public ObservableCollection<ITerran> Terrans { get => terrans; set { terrans = value; } }

        public int Turns { get; set; }
        public int SelectedItem { get; set; }

        public string Console { get; set; }

        public ICommand Shoot
        {
            get
            {
                if (myCommand == null)
                {
                    myCommand = new RelayCommand<object>(ShootExecute, CanShootExecute);
                }
                return myCommand;
            }

        }

        //Shoot button that takes the demage and the selected Zerg from the parameter
        private void ShootExecute(object parameter)
        {
            var tAttack = int.Parse(parameter.ToString().Split('-')[0]);
            var tName = parameter.ToString().Split('-')[1];
            var newZerg = gameplayOperator.ShootZerg(parameter.ToString());
            this.SwitchOldAndNewZerg(newZerg,tAttack);
            if (newZerg.Health < 0)
            {
                newZerg.Health = 0;
            }
            this.Console += this.consoleLogger.ShootLog(tName, tAttack, newZerg.Name, newZerg.Health);
            this.Console += this.consoleLogger.TurnLog(this.Turns);
            this.SelectedItem++;

            this.Turns--;
            if (this.Turns <= 0)
            {
                ZergTurn();
                this.Turns = this.Terrans.Count;
                this.SelectedItem = 0;
            }
            gameplayOperator.CheckGameOver(this.Terrans.Count, this.Zergs.Count);
        }
        private bool CanShootExecute(object parameter) =>
            CommandHelper.CheckParameter(parameter);

        //It deals the given damage to a new zerg unit and switches it with a old zerg unit that used to be equal to it
        private void SwitchOldAndNewZerg(IZerg newZerg, int dmg)
        {
            var oldZerg = this.Zergs.Where(z => z.Health == newZerg.Health).FirstOrDefault();
            newZerg.Health -= dmg;
            this.Zergs.Remove(oldZerg);
            if (newZerg.Health > 0)
            {
                this.Zergs.Insert(0, newZerg);
            }
        }

        
        //Zerg Deals Demage at the end of the turn(It doesnt check the range)
        private async void ZergTurn()
        {
            this.Console += consoleLogger.ZergTurnStart();
            var terrans = new List<ITerran>(this.Terrans);
            var zergs = new List<IZerg>(this.Zergs);
            foreach (var zerg in zergs)
            {
                await Task.Delay(100);
                Random rnd = new Random();
                int r = rnd.Next(terrans.Count);
                terrans[r].Health -= zerg.Attack;
                if (terrans[r].Health <= 0)
                {
                    this.Console += consoleLogger.ZergKilledLog(terrans[r].Name, terrans[r].Health, zerg.Name);
                    terrans.RemoveAt(r);
                    this.Terrans = new ObservableCollection<ITerran>(terrans);
                    continue;
                }
                this.Terrans = new ObservableCollection<ITerran>(terrans);
                this.Console += consoleLogger.ZergAttackLog(terrans[r].Name, terrans[r].Health, zerg.Name, zerg.Attack);
            }
        }
    }
}
