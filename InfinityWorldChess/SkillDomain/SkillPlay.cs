using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.UgfHexMap;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain
{
    public class SkillPlay:HexUnitPlay
    {
        [SerializeField] private float MidTime;

        private bool _effected = true;
        protected override void Update()
        {
            LastTime -= Time.deltaTime;

            if (!_effected && MidTime > LastTime)
            {
                BattleScope.Instance.State = BattleFlowState.OnEffectTrig;
                _effected = true;
            }
            
            if (LastTime < 0)
                EndPlay();
            else
                OnUpdate();
        }

        protected override void OnUpdate()
        {
            
        }

        public override void Play(UgfUnit unit, UgfCell targetCell)
        {
            base.Play(unit, targetCell);
            _effected = false;
        }
    }
}