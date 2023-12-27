using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.ButtonComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuItemTab: TabPanel
    {
        [SerializeField] private Table BuffTable;
        [SerializeField] private Table ItemTable;
        [SerializeField] private EquipmentEditor EquipmentEditor;
        
        private GameMenuTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameScope.Instance.Get<GameMenuTabService>();
            base.Awake();
            PlayerGameContext player = GameScope.Instance.Player;
            
        }

        public override void RefreshTab()
        {
            PlayerGameContext player = GameScope.Instance.Player;
            TableButtonDelegate<IItem> itd = ItemTable
                .AutoSetButtonTable<IItem,ItemSorters,ItemFilters,PlayerItemButtons>(
                    player.Role.Item.All());
            ItemQuantityComponent.SetItem(itd.TableDelegate);

            BuffTable.AutoSetTable(
                player.Role.Buffs.AllVisible());

            EquipmentEditor.Bind(player.Role);
        }
    }
}