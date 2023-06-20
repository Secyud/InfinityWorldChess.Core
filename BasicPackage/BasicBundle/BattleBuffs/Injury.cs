using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class Injury : PropertyBuffBase.WithTimeRecord, IDeBuff
    {
        protected override BodyType Type => BodyType.Living;

        public override string ShowName => "内伤";
    }
}