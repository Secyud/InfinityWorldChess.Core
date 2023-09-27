using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
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
            SkillObservedService service = BattleScope.Instance.Get<SkillObservedService>();

            return service.Skill.Skill.ShowName.Contains(NameContain);
        }
    }
}