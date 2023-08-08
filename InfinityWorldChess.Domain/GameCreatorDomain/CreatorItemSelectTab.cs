using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorItemSelectTab : MonoBehaviour
    {

        [SerializeField] private Table Table;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorItemSelectTab), gameObject);
        }
        protected void Start()
        {
            Table.AutoSetMultiSelectTable<IItem, ItemSorters, ItemFilters>(
                U.Get<IItemGlobalService>().List,
                IwcAb.Instance.VerticalCellInk.Value,
                GameCreatorScope.Instance.Role.Item);
        }
    }
}