using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuItemTab: MonoBehaviour
    {
        [SerializeField] private Table BuffTable;
        [SerializeField] private Table ItemTable;
        [SerializeField] private EquipmentEditor EquipmentEditor;
        
        private GameMenuTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new GameMenuTabItem(nameof(GameMenuItemTab), gameObject, Refresh);
        }

        private void Refresh()
        {
            PlayerGameContext player = GameScope.Instance.Player;
            
            ItemTable.AutoSetButtonTable<IItem,ItemSorters,ItemFilters,PlayerItemButtons>(
                player.Role.Item,
                IwcAb.Instance.VerticalCellInk.Value);

            BuffTable.AutoSetTable(
                player.Role.Buffs.GetVisibleBuff(),
                IwcAb.Instance.VerticalCellInk.Value);

            EquipmentEditor.Bind(player.Role);
        }
    }
}