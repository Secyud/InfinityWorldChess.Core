using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.PagerComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class RoleMessageViewerBase : EditorBase<BattleRole>
    {
        [SerializeField] private ValueViewer Health;
        [SerializeField] private ValueViewer Energy;
        [SerializeField] private ValueViewer Execution;
        [SerializeField] private Table BuffContent;
        [SerializeField] private AvatarEditor Avatar;

        private TableDelegate<IBuffShowable<BattleRole>> _buffDelegate;

        protected override void ClearUi()
        {
            gameObject.SetActive(false);
        }

        protected override void InitData()
        {
            gameObject.SetActive(true);
            Avatar.Bind(Property.Role.Basic);
            _buffDelegate = BuffContent.AutoSetTable<IBuffShowable<BattleRole>>(
                null, IwcAb.Instance.VerticalCellInk.Value);
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
            foreach (IBuffShowable<BattleRole> buff in Property.GetVisibleBuff())
                _buffDelegate.Items.Add(buff);
            Pager.CheckComponent(BuffContent);
        }

        protected void RefreshState()
        {
            enabled = true;
        }
    }
}