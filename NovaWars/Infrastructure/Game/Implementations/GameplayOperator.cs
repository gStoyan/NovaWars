using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.Utilities.Save;
using NovaWars.Utilities.Save.Implementations;
using NovaWars.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class GameplayOperator : IGameplayOperator
    {
        private IZergShooter zergShooter;
        private ITerranShooter terranShooter;
        private ISaver saver;
        
        public GameplayOperator()
        {
            this.saver = new Saver();
            this.terranShooter = new TerranShooter();

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

        public IZerg ShootZerg(string param) 
        {
            this.zergShooter = new ZergShooter(param);
            return this.zergShooter.CreateNewZerg();
        } 

        public List<ITerran> EndRound(List<ITerran> terrans, List<IZerg> zergs) =>
            this.terranShooter.ShootTerran(terrans, zergs);

        public void Save(int level, ObservableCollection<ITerran> terrans)
        {
            this.saver.SaveLevel(level, terrans.ToList());
        }
    }
}
