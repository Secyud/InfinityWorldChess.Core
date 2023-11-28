using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    public class AddBuffToBattleRole :AccessorWithTemplate<IBattleRoleBuff>,
        IActionable<BattleRole>, IHasContent, IPropertyAttached
    {
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"为目标添加状态{Template?.Name}。");
        }

        public void Invoke(BattleRole target)
        {
            if (target is null) return;

            IBattleRoleBuff buff = Accessor?.Value;
            if (buff is null) return;
            if (Property is ActiveSkillBase activeSkillBase)
            {
                buff.Origin = activeSkillBase.Role;
            }
            buff.Target = target;
            buff.Property = Property;
            target.Buffs.Install(buff);
        }
        public IAttachProperty Property { get; set; }
    }
}