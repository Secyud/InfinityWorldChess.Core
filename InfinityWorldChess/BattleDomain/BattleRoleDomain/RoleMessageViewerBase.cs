using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class RoleMessageViewerBase : EditorBase<BattleRole>
    {
        [SerializeField] private ValueViewer Health;
        [SerializeField] private ValueViewer Energy;
        [SerializeField] private ValueViewer Execution;
        [SerializeField] private Table BuffContent;
        [SerializeField] private AvatarEditor Avatar;

        private TableDelegate<IBattleRoleBuff> _buffDelegate;

        protected override void ClearUi()
        {
            gameObject.SetActive(false);
        }

        protected override void InitData()
        {
            gameObject.SetActive(true);
            Avatar.OnInitialize(Property.Role.Basic);
            _buffDelegate = BuffContent.AutoSetTable<IBattleRoleBuff>(null);
        }

        private void LateUpdate()
        {
            enabled = false;
            if (Property is null)
                return;
            Health.SetValue(Property.HealthValue, Property.MaxHealthValue);
            Energy.SetValue(Property.EnergyValue, Property.MaxEnergyValue);
            Execution.SetValue(Property.ExecutionValue, SharedConsts.MaxExecutionValue);
            _buffDelegate.Items.Clear();
            foreach (IBattleRoleBuff buff in Property.Buffs.All())
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