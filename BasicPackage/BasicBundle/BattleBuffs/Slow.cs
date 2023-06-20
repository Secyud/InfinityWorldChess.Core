using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class Slow : PropertyBuffBase.WithTimeRecord, IDeBuff
    {
        protected override BodyType Type => BodyType.Nimble;

        public override string ShowName => "缓速";
    }
}