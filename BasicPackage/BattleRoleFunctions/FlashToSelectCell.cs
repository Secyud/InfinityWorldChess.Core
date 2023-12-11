using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

namespace InfinityWorldChess.BattleRoleFunctions
{
    [ID("2A14E687-EBA2-409A-D09E-AD1C75A55D77")]
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
                    target.DirectionTo(activeSkillBase.Cell);
                target.Location = activeSkillBase.Cell;
                target.Direction = direction;
            }
        }
    }
}