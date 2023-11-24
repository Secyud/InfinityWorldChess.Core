using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    public class RoleAddPassiveSkill:AccessorWithTemplate<IPassiveSkill>,
        IActionable<Role> ,IHasContent
    {
        public void Invoke(Role target)
        {
            target.PassiveSkill.TryAddLearnedSkill(Accessor.Value);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"可习得内功{Template.Name}。");
            base.SetContent(transform);
        }
    }
}