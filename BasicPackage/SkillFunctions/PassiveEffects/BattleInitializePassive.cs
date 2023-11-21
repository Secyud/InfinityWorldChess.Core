using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleFunctions;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class BattleInitializePassive: IPassiveSkillAttached
    {
        [field:S] public IOnBattleRoleInitialize Initialize { get; set; }
        public PassiveSkill Skill { get; set; }
        public void SetContent(Transform transform)
        {
            if (Initialize is IHasContent content)
            {
                content.SetContent(transform);
            }
        }

        public void Install(Role target)
        {
            if (Initialize is IOnBattleRoleInitializeP action)
            {
                action.Property = Skill;
            }
            target.IdBuffs.BattleInitializes.Add(Initialize);
        }

        public void UnInstall(Role target)
        {
            target.IdBuffs.BattleInitializes.Remove(Initialize);
        }
    }
}