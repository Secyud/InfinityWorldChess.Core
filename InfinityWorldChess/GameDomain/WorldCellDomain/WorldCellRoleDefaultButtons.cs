using InfinityWorldChess.BattleDomain.LightBattle;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class WorldCellRoleDefaultButtons
    {
        public static void RegistrarButtons(InteractionButtons buttons)
        {
            buttons.Register(new LightBattleButton());
        }
    }
}