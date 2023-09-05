using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.SkillDomain
{
    public class ActiveSkillEffectDelegate:IActiveSkillEffect
    {
        public string ShowDescription => Effect.ShowDescription;
        [field:S] public ISkillCastEffect Effect { get; set; }
        
        public void Cast(IActiveSkill skill, BattleRole role, HexCell releasePosition, ISkillRange range)
        {
            Effect?.Cast(role,releasePosition,range);
        }
    }
}