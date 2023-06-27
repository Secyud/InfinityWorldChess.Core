using System;
using System.Collections.Generic;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.RoleDomain.TableComponents;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InfinityWorldChess.GlobalDomain
{
    [Registry]
    public class GlobalScope : DependencyScopeProvider
    {
        private static IMonoContainer<SelectableTable> _selectTable;
        private static IMonoContainer<BodyPartSelectComponent> _bodyPartSelect;

        private IwcTableHelperSh<IItem, ItemTf> _itemTableHelper;
        private RoleSelectTableHelper _roleSelectTableHelper;
        public static GlobalScope Instance { get; private set; }
        
        public GlobalScope(IwcAb ab)
        {
            _selectTable ??= MonoContainer<SelectableTable>.Create(ab);
            _bodyPartSelect ??= MonoContainer<BodyPartSelectComponent>.Create(ab);
            Instance = this;
        }

        public override void Dispose()
        {
            Instance = null;
        }

        public virtual void OnItemSelectionOpen(List<IItem> items, UnityAction<IItem> action)
        {
            _selectTable.Create();
            _itemTableHelper = new IwcTableHelperSh<IItem, ItemTf>
            {
                CallBackAction = action
            };
            _itemTableHelper.OnInitialize(
                _selectTable.Value,
                IwcAb.Instance.VerticalCellInk.Value,
                items
            );
            _selectTable.Value.CancelAction += OnItemSelectionClose;
        }

        public virtual void OnItemSelectionClose()
        {
            _selectTable.Destroy();
            _itemTableHelper = null;
        }
        
        
        public virtual void OnRoleSelectionOpen(List<Role> roles, UnityAction<Role> action)
        {
            _selectTable.Create();
            _selectTable.Value.ShowCellContent.GetComponent<GridLayoutGroup>().cellSize =
                new Vector2(72, 108);
            _roleSelectTableHelper = new RoleSelectTableHelper
            {
                CallBackAction = action
            };

            _roleSelectTableHelper.OnInitialize(
                _selectTable.Value,
                IwcAb.Instance.RoleAvatarCell.Value,
                roles
            );
            _selectTable.Value.CancelAction += OnRoleSelectionClose;
        }

        public virtual void OnRoleSelectionClose()
        {
            _selectTable.Destroy();
            _roleSelectTableHelper = null;
        }

        public virtual void OnBodySelectionOpen(byte code, Action<int> action)
        {
            _bodyPartSelect.Create();
            _bodyPartSelect.Value.OnInitialize(code);
            _bodyPartSelect.Value.EnsureAction += action;
        }

        public virtual void OnBodySelectionClose()
        {
            _selectTable.Destroy();
        }

    }
}