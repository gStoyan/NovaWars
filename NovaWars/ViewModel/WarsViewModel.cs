
using NovaWars.Infrastructure.Game.Implementations;
using NovaWars.Infrastructure.Levels;
using NovaWars.Model;
using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NovaWars.ViewModel
{
    public class WarsViewModel : BaseViewModel
    {
        public ICommand UpdateCommand { get; set; }
        private int level1 = 1;
        private RelayCommand<object> myCommand;
        private Initialiser initialiser;
        private ObservableCollection<IZerg> zergs;
        private ObservableCollection<ITerran> terrans;

        public WarsViewModel()
        {
            this.initialiser = new Initialiser();
            this.zergs = new ObservableCollection<IZerg>(this.initialiser.InitialiseLevel(level1).Values.First());
            this.terrans = new ObservableCollection<ITerran>(this.initialiser.InitialiseLevel(level1).Keys.First());

        }
        public ObservableCollection<IZerg> Zergs { get => zergs; set { zergs = value; } }

        public ObservableCollection<ITerran> Terrans { get => terrans; set { terrans = value; } }

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
            var zergShooter = new ZergShooter(parameter.ToString());
            var newZerg = zergShooter.CreateNewZerg();
            int damage = int.Parse(parameter.ToString().Split('-')[0]);
            this.SwitchOldAndNewZerg(newZerg, damage);
        }
        private bool CanShootExecute(object parameter) =>
            CommandHelper.CheckParameter(parameter);

        //It deals the given damage to a new zerg unit and switches it with a old zerg unit that used to be equal to it
        private void SwitchOldAndNewZerg(IZerg newZerg, int damage)
        {
            var oldZerg = this.Zergs.Where(z => z.Health == newZerg.Health).FirstOrDefault();
            this.Zergs.Remove(oldZerg);
            newZerg.Health -= damage; // Damage from nova or terran
            if (newZerg.Health > 0)
            {
                this.Zergs.Insert(0, newZerg);
            }
        }

        //Zerg Deals Demage at the end of the turn(It doesnt check the range
        private void SwitchOldAndNewArmies()
        {
            var terranShooter = new TerranShooter();
            var terransList = new List<ITerran>(this.Terrans);
            var zergsList = new List<IZerg>(this.Zergs);
            var armies = terranShooter.ShootTerran(terransList, zergsList);

            this.Zergs = new ObservableCollection<IZerg>(armies.Values.First());
            this.Terrans = new ObservableCollection<ITerran>(armies.Keys.First());
        }
    }
}
