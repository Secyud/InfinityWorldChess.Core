using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions
{
    /// <summary>
    /// effect run if current skill name contains the str NameContain
    /// </summary>
    public class NameLimit:ITriggerLimit
    {
        [field:S] public string NameContain { get; set; }
        public bool CheckLimit(SkillInteraction target)
        {
            CoreSkillContainer coreSkill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;

            if (coreSkill is not null &&
                coreSkill.CoreSkill.Name.Contains(NameContain))
            {
                return true;
            }
            
            FormSkillContainer formSkill =
                BattleScope.Instance.Get<FormSkillActionService>().FormSkill;
            
            if (formSkill is not null &&
                formSkill.FormSkill.Name.Contains(NameContain))
            {
                return true;
            }
            
            return false;
        }
    }
}