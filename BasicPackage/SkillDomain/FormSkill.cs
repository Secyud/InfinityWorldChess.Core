using System.Linq;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain.SkillRangeDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
    public class FormSkill : ActiveSkillBase,IFormSkill
    {
        [field:S]public byte Type { get; set; }
        [field:S]public byte State { get; set; }


        public override ISkillRange GetCastPositionRange(
            BattleRole role, IActiveSkill skill)
        {
            ISkillRange range = base.GetCastPositionRange(role, skill);
            return new SkillRange(range.Value.Where(u=>!u.Unit).ToArray());

        }
    }
}