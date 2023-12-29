using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    /// <summary>
    /// 操控技能效果在何时触发，触发后界面应该处于可操控状态。
    /// 如果具有技能动作，则技能动作完成后才应该处于可操控状态。
    /// </summary>
    public class SkillEffectDelegate : UgfUnitEffectDelegate
    {
        public static SkillEffectDelegate Default { get; } = new()
        {
            AttachOnUnit = true,
            TargetLocated = false,
            TriggerDistance = 0.5f,
            RemainDistance = 0.5f,
            TriggerTime = 0.3f,
            RemainTime = 0.3f,
        };


        [field: S] public bool AttachOnUnit { get; set; }
        [field: S] public bool TargetLocated { get; set; }
        [field: S] public float TriggerDistance { get; set; }
        [field: S] public float RemainDistance { get; set; }
        [field: S] public float TriggerTime { get; set; }
        [field: S] public float RemainTime { get; set; }

        public override void OnInitialize(UgfUnitEffect effect, UgfUnit unit, UgfCell target)
        {
            base.OnInitialize(effect, unit, target);

            if (AttachOnUnit)
            {
                effect.transform.SetParent(unit.transform);
            }

            Transform trans = effect.transform;
            Vector3 t = target.Position + Vector3.up * 20;
            Vector3 s = unit.Location.Position + Vector3.up * 20;
            Vector3 direction = t - s;
            Quaternion rotation = Quaternion.LookRotation(direction);
            trans.SetPositionAndRotation(TargetLocated ? t : s, rotation);

            Sequence sequence = DOTween.Sequence();

            Vector3 position =direction * TriggerDistance + trans.position;
            DoMove(position, TriggerTime);
            sequence.AppendCallback(SetTrigger);
            DoMove(direction * RemainDistance + position, RemainTime);
            sequence.OnComplete(() => EndPlay(effect));
            sequence.Play();

            return;

            void DoMove(Vector3 tg, float time)
            {
                TweenerCore<Vector3, Vector3, VectorOptions> tw = DOTween.To(
                    () => trans.position,
                    (p) => trans.position = p, tg, time);
                tw.SetTarget(effect);
                sequence.Append(tw);
            }
        }

        private static void SetTrigger()
        {
            BattleScope.Instance.Get<BattleControlService>().TriggerEffect();
        }

        private static void EndPlay(UgfUnitEffect effect)
        {
            Object.Destroy(effect.gameObject);
        }
    }
}