using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.HexMap;
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

            if (!_effected && MidTime < LastTime)
            {
                BattleScope.Instance.State = BattleFlowState.SkillCast;
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

        public override void Play(HexUnit unit, HexCell targetCell)
        {
            base.Play(unit, targetCell);
            _effected = false;
        }
    }
}