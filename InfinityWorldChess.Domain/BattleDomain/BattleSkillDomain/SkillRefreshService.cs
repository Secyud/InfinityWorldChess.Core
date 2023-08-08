using InfinityWorldChess.BattleDomain.BattleMapDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class SkillRefreshService: RefreshService<SkillRefreshService,SkillRefreshItem>
    {
        private SkillContainer _skill;
        public SkillContainer Skill
        {
            get => _skill;
            set
            {
                if (_skill == value)
                    return;
                _skill = value;
                Refresh();
            }
        }
    }
}