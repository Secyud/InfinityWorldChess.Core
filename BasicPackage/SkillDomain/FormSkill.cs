using System.Runtime.InteropServices;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillDomain
{
    [Guid("AF1A570B-807B-7142-B5C4-966656512275")]
    public class FormSkill : ActiveSkillBase,IFormSkill
    {
        [field:S(16)]public byte Type { get; set; }
        [field:S(16)]public byte State { get; set; }
    }
}