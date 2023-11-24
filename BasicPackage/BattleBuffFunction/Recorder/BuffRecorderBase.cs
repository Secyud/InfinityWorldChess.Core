using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public abstract class BuffRecorderBase :IBuffRecorder
    {
        [field: S] public int RemoveValue { get; set; } = 1;

        public BattleRoleBuff Origin { get; set; }
        protected int Remain => Origin?.BuffRecord ?? 0 / RemoveValue;

        protected BattleContext Context => BattleScope.Instance.Context;


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
            if (Origin.Target is null)
            {
                return;
            }

            Origin.BuffRecord -= RemoveValue;

            if (Origin.BuffRecord <= 0)
            {
                Origin.Target.Buffs.UnInstall(Origin.Id);
            }
        }
    }
}