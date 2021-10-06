using NovaWars.Infrastructure.Game;
using NovaWars.Infrastructure.Game.Implementations;
using NovaWars.Infrastructure.Game.Save;
using NovaWars.Infrastructure.Game.Save.Implementations;
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

        private IZergOperator zergOperator;
        private ITerranOperator terranOperator;
        private ISaver saver;
        private RelayCommand<object> myCommand;
        private Initialiser initialiser;
        private IGameplayOperator gameplayOperator;
        private IConsoleLogger consoleLogger;
        private ObservableCollection<IZerg> zergs;
        private ObservableCollection<ITerran> terrans;

        public WarsViewModel()
        {
            this.zergOperator = new ZergOperator();
            this.terranOperator = new TerranOperator();
            this.saver = new Saver();
            this.consoleLogger = new ConsoleLogger();
            this.gameplayOperator = new GameplayOperator(zergOperator,terranOperator,saver,consoleLogger);
            this.initialiser = new Initialiser();
            this.zergs = new ObservableCollection<IZerg>(this.initialiser.InitialiseLevel(this.Level).Values.First());
            this.terrans = new ObservableCollection<ITerran>(this.saver.ReadTerranSavedFile());
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
            this.saver.SaveLevel(this.Level, this.Terrans.ToList());
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
                StartZergTurn();
                this.Turns = this.Terrans.Count;
                this.SelectedItem = 0;
            }
        }
        
        private async void StartZergTurn() //Zerg Deals Demage at the end of the turn(It doesnt check the range)
        {

            this.Console = Constants.ZergTurnStarts + this.Console;
            var terrans = new List<ITerran>(this.Terrans);
            var zergs = new List<IZerg>(this.Zergs);
            var result = this.gameplayOperator.ZergTurn(terrans, zergs);
            
            this.Terrans = new ObservableCollection<ITerran>(result.Item1);
            foreach(var log in result.Item2)
            {
                await Task.Delay(100);
                this.Console = log + this.Console;
            }
            this.Turns = this.Terrans.Count();
        }
    }
}

