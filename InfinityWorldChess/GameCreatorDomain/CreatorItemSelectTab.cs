using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using Secyud.Ugf.TableComponents.SelectComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorItemSelectTab : TabPanel
    {

        [SerializeField] private Table Table;

        private CreatorTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameCreatorScope.Instance.Get<CreatorTabService>();
            base.Awake();
        }

        protected void Start()
        {
            MultiSelectDelegate<IItem> d = Table.AutoSetMultiSelectTable<IItem, ItemSorters, ItemFilters>(
                U.Get<IItemGlobalService>().List,
                GameCreatorScope.Instance.Role.Item.All());
            ItemQuantityComponent.SetItem(d.TableDelegate);
        }
        public override void RefreshTab()
        {
            
        }

    }
}