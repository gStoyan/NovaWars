using NovaWars.Infrastructure.Game;
using NovaWars.Infrastructure.Game.Implementations;
using NovaWars.Infrastructure.Game.Save;
using NovaWars.Infrastructure.Game.Save.Implementations;
using NovaWars.Model.Terrans;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Utilities;
using NovaWars.Utilities.Console;
using NovaWars.Utilities.Console.Implementations;
using NovaWars.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NovaWars.ViewModel
{
    public class IntermissionViewModel : BaseViewModel
    {
        private IConsoleLogger consoleLogger;
        private ITerranOperator terranOperations;
        private ISaver saver;
        public IntermissionViewModel()
        {
            this.consoleLogger = new ConsoleLogger();
            this.terranOperations = new TerranOperator();
            this.saver = new Saver();


            var list = this.saver.ReadSavedFile();
            this.Terrans = new ObservableCollection<Terran>(list);
            if (Application.Current.Windows.OfType<MainWindow>().Any())
            {
                Application.Current.Windows.OfType<MainWindow>().First().Close();
            }
        }
        private RelayCommand<object> myCommand;

        public string Console { get; set; }

        public ObservableCollection<Terran> Terrans { get ; set ;  }

        public ObservableCollection<Terran> Recruits { get; set; }

        public ICommand Upgrade
        {
            get
            {
                if (myCommand == null)
                {
                    myCommand = new RelayCommand<object>(UpgradeExecute, CanUpgradeExecute);
                }
                return myCommand;
            }
        }

        private void UpgradeExecute(object parameter)
        {
            AddUpgradedUnit(parameter.ToString());

        }
        private bool CanUpgradeExecute(object parameter) => true;

        private void AddUpgradedUnit(string param)
        {
            var target = param.Split('-');
            var unit = this.Terrans
                .First(t => t.Name == target[0].Trim() && t.Health == int.Parse(target[1]));
                
            var upgrUnit = this.terranOperations.UpgradeUnit(unit);
            this.Terrans.Remove(unit);
            this.Terrans.Add(upgrUnit);

            this.Console += this.consoleLogger.UpgradeTerranUnitLog(upgrUnit.Name);
        }
    }
}
