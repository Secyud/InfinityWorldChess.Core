using InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions;
using InfinityWorldChess.RoleDomain;

namespace InfinityWorldChess.BasicBundle.BattleBuffs
{
    public class Maim : PropertyBuffBase.WithTimeRecord, IDeBuff
    {
        protected override BodyType Type => BodyType.Kiling;

        public override string ShowName => "致残";
    }
}