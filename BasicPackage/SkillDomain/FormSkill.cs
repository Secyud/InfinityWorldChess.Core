using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class FormSkill : ActiveSkillBase,IFormSkill
    {
        [field:S(16)]public byte Type { get; set; }
        [field:S(16)]public byte State { get; set; }
    }
}