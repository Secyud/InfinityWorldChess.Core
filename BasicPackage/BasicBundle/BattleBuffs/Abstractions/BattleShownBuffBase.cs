using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions
{
    public abstract class BattleShownBuffBase : IBuffShowable<BattleRole>
    {
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }
        public virtual bool Visible => true;
        public abstract string ShowName { get; }
        public abstract string ShowDescription { get; }
        public BattleRole Launcher { get; set; }

        public virtual void Install(BattleRole target)
        {
        }

        public virtual void UnInstall(BattleRole target)
        {
        }

        public virtual void Overlay(IBuff<BattleRole> finishBuff)
        {
        }

        public virtual void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }
    }
}