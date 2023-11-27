using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleInteractionDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class ExtraBattleInteraction : IActionable,IPropertyAttached,IHasContent
    {
        [field: S(258)] public IActionable<BattleInteraction> PreInteraction { get; set; }
        [field: S(259)] public IActionable<BattleInteraction> OnInteraction { get; set; }
        [field: S(260)] public IActionable<BattleInteraction> PostInteraction { get; set; }

        public BattleRoleBuff Origin { get; set; }

        public IAttachProperty Property
        {
            get => null;
            set
            {
                value.TryAttach(PreInteraction);
                value.TryAttach(OnInteraction);
                value.TryAttach(PostInteraction);
            }
        }

        // to avoid self call
        private bool _triggerState;

        public void SetContent(Transform transform)
        {
            PreInteraction.TrySetContent(transform);
            OnInteraction.TrySetContent(transform);
            PostInteraction.TrySetContent(transform);
        }

        public void Invoke()
        {
            if (_triggerState)
            {
                return;
            }

            _triggerState = true;
            
            BattleInteraction interaction = BattleInteraction
                .Create(Origin.Origin, Origin.Target);
            PreInteraction?.Invoke(interaction);
            interaction.BeforeHit();
            OnInteraction?.Invoke(interaction);
            interaction.AfterHit();
            PostInteraction?.Invoke(interaction);

            _triggerState = false;
        }
    }
}