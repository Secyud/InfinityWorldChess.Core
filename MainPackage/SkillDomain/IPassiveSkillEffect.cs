using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;

namespace InfinityWorldChess.SkillDomain
{
    /// <summary>
    /// 被动技能效果，技能效果应装备在人物身上，如果有战斗效果，
    /// 应当在战斗初始化时装备在战斗单位上。
    /// </summary>
    public interface IPassiveSkillEffect:IHasContent,IInstallable<Role>
    {
    }
}