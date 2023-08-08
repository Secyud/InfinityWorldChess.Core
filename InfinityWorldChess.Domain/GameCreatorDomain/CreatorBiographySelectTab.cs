using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBiographySelectTab :MonoBehaviour
    {

        [SerializeField] private Table Table;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorBiographySelectTab), gameObject);
        }

        protected void Start()
        {
            Table.AutoSetMultiSelectTable<IBiography, BiographySorters, BiographyFilters>(
                U.Get<IBiographyGlobalService>().Biographies.Items,
                IwcAb.Instance.VerticalCellInk.Value,
                GameCreatorScope.Instance.Biography);
        }
    }
}