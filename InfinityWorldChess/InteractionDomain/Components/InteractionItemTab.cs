using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.ButtonComponents;
using UnityEngine;

namespace InfinityWorldChess.InteractionDomain
{
    public class InteractionItemTab: TabPanel
    {
        [SerializeField] private Table ItemTable;
        [SerializeField] private EquipmentEditor EquipmentEditor;
        
        private InteractionTabService _service; 
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = InteractionScope.Instance.Get<InteractionTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
            Role role = _service.InteractionRole;

            TableButtonDelegate<IItem> itd = ItemTable.AutoSetButtonTable
                <IItem,ItemSorters,ItemFilters,PlayerItemButtons>(role.Item.All());
            ItemQuantityComponent.SetItem(itd.TableDelegate);

            EquipmentEditor.Bind(role);
        }
    }
}