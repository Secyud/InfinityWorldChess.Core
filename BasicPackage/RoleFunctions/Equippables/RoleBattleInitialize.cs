using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleBattleInitialize: IEquippable<Role>,IHasContent
    {
        [field:S] public IActionable<BattleRole> Initialize { get; set; }
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
            Skill.Attach(Initialize);
            target.Buffs.BattleInitializes.Add(Initialize);
        }

        public void UnInstall(Role target)
        {
            target.Buffs.BattleInitializes.Remove(Initialize);
        }
    }
}