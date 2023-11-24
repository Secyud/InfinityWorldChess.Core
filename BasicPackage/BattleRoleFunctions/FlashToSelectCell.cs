using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    public class FlashToSelectCell : IActionable<BattleRole>, IHasContent, IPropertyAttached
    {
        public IAttachProperty Property { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("闪现至目标点。");
        }

        public void Invoke(BattleRole target)
        {
            if (Property is ActiveSkillBase activeSkillBase && !activeSkillBase.Cell.Unit)
            {
                HexDirection direction = 
                    target.Unit.DirectionTo(activeSkillBase.Cell);
                target.Unit.Location = activeSkillBase.Cell;
                target.Direction = direction;
            }
        }
    }
}