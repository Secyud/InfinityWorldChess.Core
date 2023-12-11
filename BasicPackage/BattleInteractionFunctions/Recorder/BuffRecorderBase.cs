using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleInteractionFunctions
{
    public abstract class BuffRecorderBase : IBuffRecorder,IBuffAttached
    {
        [field: S] public int RemoveValue { get; set; } = 1;

        public IBattleUnitBuff Buff { get; set; }
        
        protected int Remain => (Buff?.BuffRecord ?? 0) / RemoveValue;

        protected static BattleContext Context => BattleScope.Instance.Context;


        public virtual void InstallFrom(BattleUnit target)
        {
        }

        public virtual void UnInstallFrom(BattleUnit target)
        {
        }

        public virtual void SetContent(Transform transform)
        {
        }

        protected virtual void CalculateRemove()
        {
            if (Buff.Target is null)
            {
                return;
            }

            Buff.BuffRecord -= RemoveValue;

            if (Buff.BuffRecord <= 0)
            {
                Buff.Target.Buffs.UnInstall(Buff.Id);
            }
        }
    }
}