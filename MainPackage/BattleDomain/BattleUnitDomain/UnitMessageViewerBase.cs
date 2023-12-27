using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class UnitMessageViewerBase : EditorBase<BattleUnit>
    {
        [SerializeField] private ValueViewer Health;
        [SerializeField] private ValueViewer Energy;
        [SerializeField] private ValueViewer Execution;
        [SerializeField] private Table BuffContent;
        [SerializeField] private AvatarEditor Avatar;

        private TableDelegate<IBattleUnitBuff> _buffDelegate;

        protected override void ClearUi()
        {
            gameObject.SetActive(false);
        }

        protected override void InitData()
        {
            gameObject.SetActive(true);
            Avatar.OnInitialize(Property.Role.Basic);
            _buffDelegate = BuffContent.AutoSetTable<IBattleUnitBuff>(null);
        }

        private void LateUpdate()
        {
            enabled = false;
            if (Property is null)
                return;
            Health.SetValue(Property.HealthValue, Property.MaxHealthValue);
            Energy.SetValue(Property.EnergyValue, Property.MaxEnergyValue);
            Execution.SetValue(Property.ExecutionValue, MainPackageConsts.MaxExecutionValue);
            _buffDelegate.Items.Clear();
            foreach (IBattleUnitBuff buff in Property.Buffs.All())
            {
                _buffDelegate.Items.Add(buff);
            }
            BuffContent.Refresh();
        }

        protected void RefreshState()
        {
            enabled = true;
        }
    }
}