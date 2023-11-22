using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class ExtraInteractionAction : IBuffActionEffect
    {
        [field: S(258)] public ISkillInteractionEffect PreInteraction { get; set; }
        [field: S(259)] public ISkillInteractionEffect OnInteraction { get; set; }
        [field: S(260)] public ISkillInteractionEffect PostInteraction { get; set; }
        
        // to avoid self call
        private bool _triggerState;

        public SkillBuff BelongBuff { get; set; }

        public void Active()
        {
            if (_triggerState)
            {
                return;
            }

            _triggerState = true;

            SetAttached(PreInteraction);
            SetAttached(OnInteraction);
            SetAttached(PostInteraction);
            
            SkillInteraction interaction = SkillInteraction
                .Create(BelongBuff.Origin, BelongBuff.Target);
            PreInteraction?.Invoke(interaction);
            interaction.BeforeHit();
            OnInteraction?.Invoke(interaction);
            interaction.AfterHit();
            PostInteraction?.Invoke(interaction);

            _triggerState = false;
        }
        
        private void SetAttached(IActiveSkillAttached attached)
        {
            if (attached is not null)
            {
                attached.BelongSkill = BelongBuff.Property as ActiveSkillBase;
            }
        }

        public void SetContent(Transform transform)
        {
            PreInteraction?.SetContent(transform);
            OnInteraction?.SetContent(transform);
            PostInteraction?.SetContent(transform);
        }
    }
}