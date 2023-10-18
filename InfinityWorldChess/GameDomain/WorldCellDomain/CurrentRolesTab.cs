#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.InteractionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentRolesTab : TabPanel
    {
        [SerializeField] private Table RoleTable;

        private List<Role> _showRoles;
        private CurrentTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameScope.Instance.Get<CurrentTabService>();
            base.Awake();
            _showRoles = new List<Role>();
            TableDelegate<Role> td = RoleTable.AutoSetFunctionTable
                <Role, RoleSorters, RoleFilters>(
                    _showRoles, AvatarEditor.SetCell);
            td.BindInitAction(InitSelectRole);
        }

        public override void RefreshTab()
        {
            _showRoles.Clear();

            WorldCell cell = GameScope.Instance.Get<CurrentTabService>().Cell;
            if (cell)
            {
                _showRoles.AddRange(cell.InRoles.Where(u => u != GameScope.Instance.Player.Role));
            }

            RoleTable.Refresh();
        }

        private void InitSelectRole(TableCell cell, Role item)
        {
            SButton button = cell.gameObject.GetOrAddComponent<SButton>();
            button.onClick.AddListener(() => SelectRole(item));
        }

        private void SelectRole(Role role)
        {
            InteractionScope.Instance.SetSelectRole(role);
        }
    }
}