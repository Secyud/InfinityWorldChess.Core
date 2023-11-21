using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public class SkillBuff : IBuffShowable<BattleRole>
    {
        [field: S(0)] public int Id { get; set; }
        [field: S(1)] public string Name { get; set; }
        [field: S(2)] public bool Visible { get; set; }
        [field: S(2)] public int BuffRecord { get; set; }
        [field: S(3)] public string Description { get; set; }
        [field: S(4)] public IObjectAccessor<Sprite> Icon { get; set; }

        // the effect of buff
        [field: S(5)] public IEffectEffect BuffEffect { get; set; }

        // decided when buff remove, null if not remove
        [field: S(6)] public EffectRecorder EffectRecorder { get; set; }

        public BattleRole Role { get; set; }

        public virtual void Install(BattleRole target)
        {
            Role = target;
            BuffEffect?.Install(target);
            EffectRecorder?.Install(target);
        }

        public virtual void UnInstall(BattleRole target)
        {
            Role = null;
            BuffEffect?.UnInstall(target);
            EffectRecorder?.UnInstall(target);
        }

        public virtual void Overlay(IBuff<BattleRole> finishBuff)
        {
            BuffEffect?.Overlay(finishBuff);
        }

        public virtual void SetProperty(IBuffProperty skill)
        {
            SetAttach(BuffEffect, skill);
            SetAttach(EffectRecorder, skill);
        }

        private void SetAttach(IBuffAttached attached, IBuffProperty property)
        {
            if (attached is not null)
            {
                attached.Buff = this;
                attached.SetProperty(property);
            }
        }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);

            if (BuffEffect is IHasContent effect)
            {
                effect.SetContent(transform);
            }

            if (EffectRecorder is IHasContent recorder)
            {
                recorder.SetContent(transform);
            }
        }
    }
}