using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicEffectBundle
{
    /// <summary>
    /// effect run if current skill name contains the str NameContain
    /// </summary>
    public class NameLimit:IEffectLimit
    {
        [field:S] public string NameContain { get; set; }
        public bool CheckLimit(SkillInteraction target)
        {
            CoreSkillContainer coreSkill =
                BattleScope.Instance.Get<CoreSkillActionService>().CoreSkill;

            if (coreSkill is not null &&
                coreSkill.CoreSkill.ShowName.Contains(NameContain))
            {
                return true;
            }
            
            FormSkillContainer formSkill =
                BattleScope.Instance.Get<FormSkillActionService>().FormSkill;
            
            if (formSkill is not null &&
                formSkill.FormSkill.ShowName.Contains(NameContain))
            {
                return true;
            }
            
            return false;
        }
    }
}