using System.Runtime.CompilerServices;

namespace NovaWars.Utilities.Console.Implementations
{
    public class ConsoleLogger : IConsoleLogger
    {

        public string ShootLog(string tName, int tAttack, string zName, int zHealth) {
            zHealth = zHealth < 0 ? 0 : zHealth;
           return $"> {tName} dealt {tAttack} dmg to {zName}({zHealth} remaining hp) \n ";
        }

        public string TurnLog(int turns) => $"> Turns Remaining: {turns} \n";

        public string ZergAttackLog(string tName, int tHealth, string zName, int zAttack) =>
             $"> {zName} dealt {zAttack} dmg to {tName}({tHealth} remaining hp) \n ";

        public string ZergKilledLog(string tName, int tHealth, string zName) =>
            $"> {zName} killed {tName}({tHealth} overkill)\n";

        public string ZergTurnStart() =>
            $"> !\n> The zerg are attacking...\n";
    }
}
