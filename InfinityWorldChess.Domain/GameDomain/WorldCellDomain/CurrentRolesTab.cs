#region

using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.FilterComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentRolesTab : MonoBehaviour
    {
        [SerializeField] private Table RoleTable;

        private PlayerGameContext _playerGameContext;

        private List<Role> _showRoles;

        private CurrentTabItem _item;

        private void Awake()
        {
            if (_showRoles is null)
            {
                _showRoles ??= new List<Role>();
                RoleTable.AutoSetButtonTable<Role, RoleSorters, RoleFilters, WorldCellRoleButtons>(
                    _showRoles, IwcAb.Instance.RoleAvatarCell.Value, AvatarEditor.SetCell);
            }

            _item ??= new CurrentTabItem(nameof(SelectMessageViewer), gameObject, Refresh);
        }

        private void Refresh()
        {
            _showRoles.Clear();

            WorldCell cell = GameScope.Instance.Get<CurrentTabService>().Cell;
            if (cell is not null)
            {
                _showRoles.AddRange(cell.InRoles.Where(u => u != _playerGameContext.Role));
                Filter.CheckComponent(RoleTable);
            }
        }
    }
}