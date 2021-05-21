using NovaWars.Model.Terrans.Extensions;
using NovaWars.Model.Zergs;
using NovaWars.Views;
using System.Collections.Generic;
using System.Windows;

namespace NovaWars.Infrastructure.Game.Implementations
{
    public class GameplayOperator : IGameplayOperator
    {
        private IZergShooter zergShooter;
        private ITerranShooter terranShooter;
        
        public GameplayOperator()
        {
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

        public List<ITerran> EndRound(List<ITerran> terrans, List<IZerg> zergs)
        {
            return this.terranShooter.ShootTerran(terrans, zergs);
        }
    }
}
