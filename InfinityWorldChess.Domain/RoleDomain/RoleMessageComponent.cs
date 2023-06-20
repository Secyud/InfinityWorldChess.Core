#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleMessageComponent : MonoBehaviour
    {
        [SerializeField] private RoleAvatarViewer RoleAvatarViewer;
        [SerializeField] private BasicForm BasicForm;
        [SerializeField] private BodyPartForm BodyPartForm;
        [SerializeField] private Table BuffTable;
        [SerializeField] private FunctionalTable ItemForm;
        [SerializeField] private EquipmentForm EquipmentForm;
        [SerializeField] private CoreSkillEquipView CoreSkillEquipView;
        [SerializeField] private FromSkillEquipView FromSkillEquipView;
        [SerializeField] private PassiveSkillEquipView PassiveSkillEquipView;
        [SerializeField] private SkillView SkillView;
        [SerializeField] private SButton[] PageButtons;
        private IwcTableHelperBh<IItem, ItemTf, ItemGameBf> _itemTableHelper;
        private IwcTableHelperH<IBuffCanBeShown<Role>> _roleBuffTableHelper;
        private RoleGameContext _context;

        protected virtual void Awake()
        {
            _roleBuffTableHelper = new IwcTableHelperH<IBuffCanBeShown<Role>>();
            _itemTableHelper = new IwcTableHelperBh<IItem, ItemTf, ItemGameBf>(Refresh);
        }

        public void Die()
        {
            GameScope.OnRoleMessageShutdown();
        }

        public void SetPage(int page)
        {
            if (page >= PageButtons.Length)
                return;

            PageButtons[page].onClick.Invoke();
        }

        public void Refresh()
        {
            EquipmentForm.RefreshEquipment();
            BuffTable.RefreshFilter();
            ItemForm.RefreshFilter();
            SkillView.Refresh();
        }

        public void OnInitialize(Role role)
        {
            _context ??= GameScope. RoleGameContext;
            _context.MainOperationRole = role;

            BasicForm.OnInitialize(role.Basic);

            BodyPartForm.OnInitialize(role.BodyPart);

            _roleBuffTableHelper.OnInitialize(
                BuffTable,
                IwcAb.Instance.VerticalCellInk.Value,
                role.Buffs.GetVisibleBuff()
            );

            _itemTableHelper.OnInitialize(
                ItemForm,
                IwcAb.Instance.VerticalCellInk.Value,
                role.Item
            );

            EquipmentForm.OnInitialize(role);

            CoreSkillEquipView.OnInitialize(role);

            FromSkillEquipView.OnInitialize(role);

            PassiveSkillEquipView.OnInitialize(role);

            SkillView.OnInitialize(role);

            RoleAvatarViewer.OnInitialize(role.Basic);
        }
    }
}