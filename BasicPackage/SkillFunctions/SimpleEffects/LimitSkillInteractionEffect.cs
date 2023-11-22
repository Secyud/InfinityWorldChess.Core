using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class LimitSkillInteractionEffect : ISkillInteractionEffect
    {
        [field: S] public ISkillInteractionEffect Effect { get; set; }
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

        public void Invoke(SkillInteraction interaction)
        {
            if (Limit is not null &&
                !Limit.CheckLimit(interaction))
            {
                return;
            }

            Effect?.Invoke(interaction);
        }
    }
}