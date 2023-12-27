using InfinityWorldChess.BundleDomain;
using Secyud.Ugf;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBundleSelectTab : TabPanel
    {

        [SerializeField] private Table Table;

        private CreatorTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameCreatorScope.Instance.Get<CreatorTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
        }

        protected void Start()
        {
            Table.AutoSetMultiSelectTable<IBundle, BundleSorters, BundleFilters>(
                U.Get<BundleGlobalService>().Bundles.Items,
                GameCreatorScope.Instance.Context.Bundles);
        }
    }
}