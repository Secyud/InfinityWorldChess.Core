using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public interface IBuffAttached
    {
        SkillBuff BelongBuff { get; set; }
    }
}