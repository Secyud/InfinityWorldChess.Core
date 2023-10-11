using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.BattleDomain.LightBattle
{
    public class LightBattleButton : ButtonDescriptor<Role>
    {
        public override void Invoke()
        {
            LightBattleDescriptor battle = new(GameScope.Instance.Player.Role, Target);


            BattleGlobalService battleService = U.Get<BattleGlobalService>();
            battleService.CreateBattle(battle);
        }

        public override string ShowName => "切磋";

        public override bool Visible(Role target)
        {
            return true;
        }
    }
}