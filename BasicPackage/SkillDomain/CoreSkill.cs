using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class CoreSkill : ActiveSkillBase, ICoreSkill
    {
        [field: S(16)] public byte FullCode { get; set; }
        [field: S(16)] public byte MaxLayer { get; set; }
    }
}