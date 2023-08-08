using System;
using Secyud.Ugf.RefreshComponents;

namespace InfinityWorldChess.BattleDomain.BattleSkillDomain
{
    public class SkillRefreshItem:RefreshActionItem<SkillRefreshService,SkillRefreshItem>
    {
        public SkillRefreshItem(string name, Action action) : base(name, action)
        {
        }
    }
}