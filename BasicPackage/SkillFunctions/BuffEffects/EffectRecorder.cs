using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class EffectRecorder : IEquippable<BattleRole>, IBuffAttached, IHasContent
    {
        [field: S] public int RemoveValue { get; set; }

        protected int Remain => BelongBuff?.BuffRecord ?? 0 / RemoveValue;

        protected BattleContext Context => BattleScope.Instance.Context;

        public SkillBuff BelongBuff { get; set; }

        public virtual void Install(BattleRole target)
        {
        }

        public virtual void UnInstall(BattleRole target)
        {
        }

        public virtual void SetContent(Transform transform)
        {
        }

        protected virtual void CalculateRemove()
        {
            if (BelongBuff.Target is null)
            {
                return;
            }

            BelongBuff.BuffRecord -= RemoveValue;

            if (BelongBuff.BuffRecord <= 0)
            {
                BelongBuff.Target.Buff.UnInstall(BelongBuff.Id);
            }
        }
    }
}