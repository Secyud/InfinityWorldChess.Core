using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillEffectDelegate : UgfUnitEffectDelegate
    {
        [field: S] public bool AttachOnUnit { get; set; }
        [field: S] public bool TargetLocated { get; set; }
        [field: S] public float TriggerDistance { get; set; }
        [field: S] public float RemainDistance { get; set; }
        [field: S] public float TriggerTime { get; set; }
        [field: S] public float RemainTime { get; set; }

        public override void OnInitialize(UgfUnitEffect effect, UgfUnit unit, HexCell target)
        {
            base.OnInitialize(effect, unit, target);
            
            if (AttachOnUnit)
            {
                effect.transform.SetParent(unit.transform);
            }

            Transform trans = effect.transform;
            Vector3 t = target.Position;
            Vector3 s = unit.Location.Position;
            Vector3 direction = t - s;
            Quaternion rotation = Quaternion.LookRotation(direction);
            trans.SetPositionAndRotation(TargetLocated ? t : s, rotation);
            
            Sequence sequence = DOTween.Sequence();

            Vector3 position = trans.position;
            DoMove(direction * TriggerDistance + position,TriggerTime);
            sequence.AppendCallback(SetTrigger);
            DoMove(direction * RemainDistance + position,RemainTime);
            sequence.OnComplete(EndPlay);
            sequence.Play();
            
            return;

            void DoMove(Vector3 tg, float time)
            {
                TweenerCore<Vector3, Vector3, VectorOptions> tw = DOTween.To(
                    () => trans.position, 
                    (p) => trans.position = p,tg,time);
                tw.SetTarget(effect);
                sequence.Append(tw) ;
            }
        }

        private static void SetTrigger()
        {
            BattleScope.Instance.State = BattleFlowState.OnEffectTrig;
        }

        private void EndPlay()
        {
            Object.Destroy(Effect.gameObject);
        }
    }
}