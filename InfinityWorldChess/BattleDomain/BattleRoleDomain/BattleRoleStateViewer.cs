using System;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class BattleRoleStateViewer : EditorBase<BattleRole>
    {
        private static int _num = 0;
        private static int State => _num++;

        [SerializeField] private ValueViewer Health;
        [SerializeField] private ValueViewer Energy;
        [SerializeField] private ValueViewer Execution;

        private StateObservedService _state;
        
        public Transform TargetTrans { get; set; }

        private void Awake()
        {
            _state = BattleScope.Instance.Get<StateObservedService>();
            _state.AddObserverObject(nameof(BattleRoleStateViewer) + State, RefreshState, gameObject);
        }

        private void Update()
        {
            if (TargetTrans)
            {
                transform.position = TargetTrans.position + new Vector3(0, 16, 0);
            }
        }

        protected override void InitData()
        {
            RefreshState();
        }

        protected void RefreshState()
        {
            if (Property is null)
                return;
            Health.SetValue(Property.HealthValue, Property.MaxHealthValue);
            Energy.SetValue(Property.EnergyValue, Property.MaxEnergyValue);
            Execution.SetValue(Property.ExecutionValue, SharedConsts.MaxExecutionValue);
        }
    }
}