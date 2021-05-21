using System.Runtime.CompilerServices;

namespace NovaWars.Utilities.Console
{
    public interface IConsoleLogger
    {
        string ShootLog(string tName, int tAttack, string zName, int zHealth);
        string TurnLog(int turns);

        string ZergAttackLog(string tName, int tHealth, string zName, int zAttack);

        string ZergKilledLog(string tName, int tHealth, string zName);

        string ZergTurnStart();
    }
}
