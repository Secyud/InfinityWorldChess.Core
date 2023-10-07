using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    public class FormSkill : ActiveSkillBase,IFormSkill
    {
        [field:S]public byte Type { get; set; }
        [field:S]public byte State { get; set; }
    }
}