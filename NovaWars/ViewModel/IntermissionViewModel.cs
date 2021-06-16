using NovaWars.Model.Terrans.Extensions;
using NovaWars.Utilities;
using NovaWars.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NovaWars.ViewModel
{
    public class IntermissionViewModel : BaseViewModel
    {
        public IntermissionViewModel()
        {
            if (Application.Current.Windows.OfType<MainWindow>().Any())
            {
                Application.Current.Windows.OfType<MainWindow>().First().Close();
            }
        }
        private RelayCommand<object> myCommand;

        public string Console { get; set; } = "Test";

        public ObservableCollection<ITerran> Terrans { get ; set ;  }

        public ObservableCollection<ITerran> Recruits { get; set; }

        public ICommand Next
        {
            get
            {
                if (myCommand == null)
                {
                    myCommand = new RelayCommand<object>(NextExecute, CanNextExecute);
                }
                return myCommand;
            }
        }

        private void NextExecute(object parameter)
        {

        }
        private bool CanNextExecute(object parameter) => true;
    }
}
