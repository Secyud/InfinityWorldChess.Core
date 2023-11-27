using InfinityWorldChess.BattleBuffFunction;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffDomain
{
    [ID("be8e28a4-2a16-6404-78f3-dbd3a2d1c6f7")]
    public class BattleRoleBuff : IBattleRoleBuff, IHasPriority,IHasContent
    {
        [field: S(0)] public int Id { get; set; }
        [field: S(1)] public string Name { get; set; }
        [field: S(2)] public int BuffRecord { get; set; }
        [field: S(2)] public int Priority { get; set; }
        [field: S(3)] public string Description { get; set; }
        [field: S(4)] public IObjectAccessor<Sprite> Icon { get; set; }

        // the effect of buff
        [field: S(5)] public IBuffEffect Effect { get; set; }

        // decided when buff remove, null if not remove
        [field: S(6)] public IBuffRecorder Recorder { get; set; }

        public BattleRole Target { get; set; }
        public BattleRole Origin { get; set; }
        public IAttachProperty Property
        {
            get=>null;
            set
            {
                value.TryAttach(Effect);
                value.TryAttach(Recorder);
            }
        }

        public virtual void Install(BattleRole target)
        {
            Effect?.Install(target);
            Recorder?.Install(target);
        }

        public virtual void UnInstall(BattleRole target)
        {
            Effect?.UnInstall(target);
            Recorder?.UnInstall(target);
        }


        public void Overlay(IOverlayable<BattleRole> otherOverlayable)
        {
            Effect?.Overlay(otherOverlayable);
        }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
            Effect.TrySetContent(transform);
            Recorder.TrySetContent(transform);
        }
    }
}