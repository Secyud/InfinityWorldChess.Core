using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkill : ActiveSkillBase, ICoreSkill
    {
        [field: S(16)] public byte FullCode { get; set; }
        [field: S(16)] public byte MaxLayer { get; set; }
        [field: S(17)] public byte ConditionCode { get; set; }
        [field: S(17)] public byte ConditionMask { get; set; }
    }
}