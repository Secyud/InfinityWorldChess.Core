using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.SkillFunctions
{
    public abstract class EffectRecorder : IEquippable<BattleRole>, IBuffAttached, IHasContent
    {
        [field: S] public int RemoveValue { get; set; }

        public int Remain => Buff?.BuffRecord ?? 0 / RemoveValue;

        protected BattleContext Context => BattleScope.Instance.Context;

        public SkillBuff Buff { get; set; }

        public virtual void Install(BattleRole target)
        {
        }

        public virtual void UnInstall(BattleRole target)
        {
        }

        public virtual void SetProperty(IBuffProperty property)
        {
        }

        public virtual void SetContent(Transform transform)
        {
        }

        protected virtual void CalculateRemove()
        {
            if (Buff.Role is null)
            {
                return;
            }

            Buff.BuffRecord -= RemoveValue;

            if (Buff.BuffRecord <= 0)
            {
                Buff.Role.Buff.UnInstall(Buff.Id);
            }
        }
    }
}