using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class LimitSkillActionEffect : ISkillActionEffect
    {
        [field: S] public ISkillActionEffect Effect { get; set; }
        [field: S] public ILimitCondition Limit { get; set; }

        public ActiveSkillBase BelongSkill
        {
            get => Effect?.BelongSkill;
            set
            {
                if (Effect is not null)
                {
                    Effect.BelongSkill = value;
                }
            }
        }

        public void SetContent(Transform transform)
        {
            if (Limit is IHasContent limitContent)
            {
                limitContent.SetContent(transform);
            }

            if (Effect is IHasContent content)
            {
                content.SetContent(transform);
            }
        }


        public void Invoke(BattleRole battleChess, BattleCell releasePosition)
        {
            if (Limit is not null &&
                !Limit.CheckLimit(BelongSkill))
            {
                return;
            }

            Effect?.Invoke(battleChess,releasePosition);
        }
    }
}