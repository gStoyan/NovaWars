
using NovaWars.Infrastructure.Game;
using NovaWars.Infrastructure.Game.Implementations;
using NovaWars.Infrastructure.Levels;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.Utilities;
using NovaWars.Utilities.Console;
using NovaWars.Utilities.Console.Implementations;
using NovaWars.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaWars.ViewModel
{
    public class WarsViewModel : BaseViewModel
    {
        public ICommand UpdateCommand { get; set; }
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
            this.zergs = new ObservableCollection<IZerg>(this.initialiser.InitialiseLevel(this.Level).Values.First());
            this.terrans = new ObservableCollection<ITerran>(this.initialiser.InitialiseLevel(this.Level).Keys.First().OrderByDescending(t => t.Attack));
            this.Turns = this.Terrans.Count;
            this.SelectedItem = 0;
        }
        public ObservableCollection<IZerg> Zergs { get => zergs; set { zergs = value; } }

        public ObservableCollection<ITerran> Terrans { get => terrans; set { terrans = value; } }

        public int Turns { get; set; } 
        public int SelectedItem { get; set; }
        public int Level { get; set; } = 1;

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
            var result = gameplayOperator.CreateShotZergAndLog(parameter,this.Zergs.ToList());

            this.Zergs = new ObservableCollection<IZerg>(result.Item1);

            this.Console = result.Item2 + this.Console;
            this.Console = this.consoleLogger.TurnLog(this.Turns) + this.Console;
            this.SelectedItem++;

            gameplayOperator.CheckGameOver(this.Terrans.Count, this.Zergs.Count);
            //gameplayOperator.Save(this.Level, this.Terrans);
            EndTurn();
        }

        private bool CanShootExecute(object parameter) =>
            CommandHelper.CheckParameter(parameter);

        private void EndTurn() // trigger zerg turn and reset turns to the count of terran units
        {
            this.Turns--;
            if (this.Turns <= 0)
            {
                ZergTurn();
                this.Turns = this.Terrans.Count;
                this.SelectedItem = 0;
            }
        }
        
        private async void ZergTurn() //Zerg Deals Demage at the end of the turn(It doesnt check the range)
        {

            this.Console += Constants.ZergTurnStarts;
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
                    this.Console = consoleLogger.ZergKilledLog(terrans[r].Name, terrans[r].Health, zerg.Name) + this.Console;
                    terrans.RemoveAt(r);
                    this.Terrans = new ObservableCollection<ITerran>(terrans);
                    continue;
                }
                this.Terrans = new ObservableCollection<ITerran>(terrans);
                this.Console = consoleLogger.ZergAttackLog(terrans[r].Name, terrans[r].Health, zerg.Name, zerg.Attack) + this.Console;
                this.Turns = this.Terrans.Count();
            }
        }
    }
}
