using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.InteractionDomain;

namespace InfinityWorldChess.DialogueFunctions
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