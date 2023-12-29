using InfinityWorldChess.BattleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    /// <summary>
    /// 技能释放目标
    /// </summary>
    public interface ISkillTargetInRange:IHasContent
    {
        ISkillTarget GetTargetInRange(BattleUnit battleChess, ISkillRange range);
    }
}