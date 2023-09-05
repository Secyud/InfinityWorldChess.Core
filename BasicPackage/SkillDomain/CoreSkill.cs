using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkill : ActiveSkillBase, ICoreSkill
    {
        [field: S] public byte FullCode { get; set; }
        [field: S] public byte MaxLayer { get; set; }
        [field: S] public byte ConditionCode { get; set; }
        [field: S] public byte ConditionMask { get; set; }
    }
}