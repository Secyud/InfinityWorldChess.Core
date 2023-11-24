using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleAddCoreSkill :AccessorWithTemplate<ICoreSkill>,
        IActionable<Role> ,IHasContent
    {
        public void Invoke(Role target)
        {
            target.CoreSkill.TryAddLearnedSkill(Accessor.Value);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"可习得招式{Template.Name}。");
            base.SetContent(transform);
        }
    }
}