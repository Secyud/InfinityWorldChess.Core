using InfinityWorldChess.BuffDomain;

namespace InfinityWorldChess.SkillFunctions
{
    public interface IBuffAttached
    {
        SkillBuff Buff { get; set; }
        void SetProperty(IBuffProperty property);
    }
}