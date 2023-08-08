using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBundleSelectTab : MonoBehaviour
    {

        [SerializeField] private Table Table;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorBundleSelectTab), gameObject);
        }
        protected void Start()
        {
            Table.AutoSetMultiSelectTable<IBundle, BundleSorters, BundleFilters>(
                U.Get<IBundleGlobalService>().Bundles.Items,
                IwcAb.Instance.VerticalCellInk.Value,
                GameCreatorScope.Instance.Bundles);
        }
    }
}