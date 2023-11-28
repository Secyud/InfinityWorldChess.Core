using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public class LimitSkillCellIsCenter: ILimitable<BattleInteraction>, IHasContent,IPropertyAttached
    {
        public bool CheckUseful(BattleInteraction target)
        {
            return Property is ActiveSkillBase activeSkillBase &&
                   activeSkillBase.Cell == target.Target.Unit.Location;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果位置是攻击的落点：");
        }

        public IAttachProperty Property { get; set; }
    }
}