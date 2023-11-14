using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.DialogueDomain;
using InfinityWorldChess.InteractionDomain;

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