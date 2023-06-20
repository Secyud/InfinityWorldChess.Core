using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class Heat : PropertyBuffBase.WithTimeRecord, IDeBuff
    {
        protected override BodyType Type => BodyType.Defend;

        public override string ShowName => "破甲";
    }
}