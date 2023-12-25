using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleUnitFunctions
{
    [Guid("09CDEB29-6E8F-D324-1A56-174680F456F3")]
    public class AddBuffToBattleUnit :AccessorWithTemplate<IBattleUnitBuff>,
        IActionable<BattleUnit>, IHasContent, IPropertyAttached
    {
        public override void SetContent(Transform transform)
        {
            transform.AddParagraph($"为目标添加状态{Template?.Name}。");
        }

        public void Invoke(BattleUnit target)
        {
            if (target is null) return;

            IBattleUnitBuff buff = Accessor?.Value;
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