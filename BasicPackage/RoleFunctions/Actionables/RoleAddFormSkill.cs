using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleAddFormSkill:AccessorWithTemplate<IFormSkill>,
        IActionable<Role> ,IHasContent
    {
        public void Invoke(Role target)
        {
            target.FormSkill.TryAddLearnedSkill(Accessor.Value);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"可习得变招{Template.Name}。");
            base.SetContent(transform);
        }
    }
}