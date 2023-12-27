using InfinityWorldChess.BiographyDomain;
using Secyud.Ugf;
using Secyud.Ugf.TabComponents;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBiographySelectTab :TabPanel
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
            Table.AutoSetMultiSelectTable<IBiography, BiographySorters, BiographyFilters>(
                U.Get<IBiographyGlobalService>().Biographies.Items,
                GameCreatorScope.Instance.Context.Biography);
        }

        public override void RefreshTab()
        {
        }

    }
}