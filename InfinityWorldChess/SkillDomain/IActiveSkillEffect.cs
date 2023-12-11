using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    public interface IActiveSkillEffect:IHasContent
    {
        void Cast(BattleUnit role, BattleCell releasePosition, ISkillRange range,IActiveSkill skill = null);
    }
}