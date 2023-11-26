using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillAnim : SkillAnimBase
    {
        [SerializeField] private AnimationCurve Curve;
        [SerializeField] private bool ControlRole;

        private Transform _animTransform;
        private Vector3 _s;
        private Vector3 _e;

        protected override void OnUpdate()
        {
            base.OnUpdate();
            float value = Curve.Evaluate(LastTime / PlayTime);
            _animTransform.position = _e + value * (_s - _e);
        }

        public override void Play(UgfUnit unit, UgfCell targetCell)
        {
            base.Play(unit, targetCell);

            _animTransform = ControlRole ? unit.transform : transform;
            _s = unit.Location.Position;
            _e = targetCell.Position;
        }
    }
}