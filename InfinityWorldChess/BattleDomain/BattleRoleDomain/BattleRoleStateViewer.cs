using Secyud.Ugf.EditorComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleRoleStateViewer : EditorBase<BattleRole>
    {
        private static int _num = 0;
        private static int State => _num++;

        [SerializeField] private ValueViewer Health;
        [SerializeField] private ValueViewer Energy;
        [SerializeField] private ValueViewer Execution;

        private BattleContext _context;
        private Transform _targetPosition;
        private void Awake()
        {
            _context = BattleScope.Instance.Context;
            _context.StateService.AddObserverObject(nameof(BattleRoleStateViewer) + State, RefreshState, gameObject);
        }

        private void Update()
        {
            if (_targetPosition)
            {
                transform.position = _targetPosition.position + new Vector3(0, 16, 0);
            }
        }

        protected override void InitData()
        {
            RefreshState();
            Property.StateViewer = this;
            _targetPosition = Property.transform;
        }

        protected void RefreshState()
        {
            if (Property is null)
                return;
            Health.SetValue(Property.HealthValue, Property.MaxHealthValue);
            Energy.SetValue(Property.EnergyValue, Property.MaxEnergyValue);
            Execution.SetValue(Property.ExecutionValue, IWCC.MaxExecutionValue);
        }
    }
}