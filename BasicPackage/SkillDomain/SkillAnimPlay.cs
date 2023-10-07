using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillAnimPlay:SkillPlay
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
            _animTransform.position =  _e + value * (_s - _e);
        }

        public override void Play(HexUnit unit, HexCell targetCell)
        {
            base.Play(unit, targetCell);

            _animTransform = ControlRole ? unit.transform : transform;
            _s = unit.Location.Position;
            _e = targetCell.Position;
        }
    }
}