using System.Runtime.InteropServices;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    [Guid("E37F8422-E782-8987-6C9D-6C313FCF9E2F")]
    public class LimitSkillCellIsCenter: ILimitable<BattleInteraction>, IHasContent,IPropertyAttached
    {
        public bool CheckUseful(BattleInteraction target)
        {
            return Property is ActiveSkillBase activeSkillBase &&
                   activeSkillBase.Cell == target.Target.Location;
        }

        public void SetContent(Transform transform)
        {
            transform.AddParagraph("如果位置是攻击的落点：");
        }

        public IAttachProperty Property { get; set; }
    }
}