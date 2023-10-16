using InfinityWorldChess.BattleDomain.LightBattle;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.InteractionDomain.ChatDomain;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class WorldCellRoleDefaultButtons
    {
        public static void RegistrarButtons(InteractionButtons buttons)
        {
            buttons.RegisterList(
                new ChatButtonDescriptor(),
                new LightBattleButton());
        }
    }
}