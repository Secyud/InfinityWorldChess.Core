#region

using System.Linq;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public class GameCreatorComponent : MonoBehaviour
	{
		[SerializeField] private NameEditor NameEditor; //2
		[SerializeField] private BirthEditor BirthEditor;
		[SerializeField] private NatureEditor NatureEditor;
		[SerializeField] private AvatarEditor AvatarEditor;
		[SerializeField] private MultiSelectTable ActivityBundleMultiSelectTable;
		[SerializeField] private MultiSelectTable BiographyMultiSelectTable;
		[SerializeField] private MultiSelectTable ItemMultiSelectTable;
		[SerializeField] private PlayerSettingEditor PlayerSettingEditor;
		[SerializeField] private WorldSettingEditor WorldSettingEditor;

		private MultiSelectTableHelper<IBundle, NormalCell, BundleTf, IwcTableHelperFh<IBundle, BundleTf>>
			_bundleMultiSelectTableHelper;

		private MultiSelectTableHelper<IBiography, NormalCell, BiographyTf,
				IwcTableHelperFh<IBiography, BiographyTf>>
			_biographyMultiSelectTableHelper;

		private CreatorScope _scope;
		private ItemMultiSelectTableHelper _itemMultiSelectTableHelper;

		private void Awake()
		{
			_biographyMultiSelectTableHelper =
				new MultiSelectTableHelper<
					IBiography, NormalCell, BiographyTf, IwcTableHelperFh<IBiography, BiographyTf>>(
					new IwcTableHelperFh<IBiography, BiographyTf>(),
					new IwcTableHelperFh<IBiography, BiographyTf>()
				);
			_bundleMultiSelectTableHelper =
				new MultiSelectTableHelper<
					IBundle, NormalCell, BundleTf, IwcTableHelperFh<IBundle, BundleTf>>(
					new IwcTableHelperFh<IBundle, BundleTf>(),
					new IwcTableHelperFh<IBundle, BundleTf>()
				);
			_itemMultiSelectTableHelper = new ItemMultiSelectTableHelper();
		}

		private void Start()
		{
			_scope ??= CreatorScope.Instance;
			Role.BasicProperty basic = _scope.Basic;

			NameEditor.OnInitialize(basic);
			BirthEditor.OnInitialize(basic);
			NatureEditor.OnInitialize(_scope.Nature);
			AvatarEditor.OnInitialize(basic);

			_bundleMultiSelectTableHelper.OnInitialize(
				ActivityBundleMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				U.Get<IBundleGlobalService>().Bundles.Get().ToList(),
				_scope.Bundles
			);

			_biographyMultiSelectTableHelper.OnInitialize(
				BiographyMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				U.Get<IBiographyGlobalService>().Biographies.Get().ToList(),
				_scope.Biography
			);

			_itemMultiSelectTableHelper.OnInitialize(
				ItemMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				U.Get<IwcItemGlobalService>().List,
				_scope.Item
			);

			PlayerSettingEditor.OnInitialize(_scope.PlayerSetting);

			WorldSettingEditor.OnInitialize(_scope.WorldSetting);
		}

		public void SetGender(bool female)
		{
			_scope.Basic.Female = female;
			AvatarEditor.OnInitialize(_scope.Basic);
		}

		public void EnterGame()
		{
			if (!NameEditor.Check())
				return;
			
			IwcAb.Instance.LoadingPanelInk.Instantiate();

			U.Factory.GameInitialize();
		}

		public void ReturnMainMenu()
		{
			U.Factory.Application.DependencyManager.CreateScope<MainMenuScope>();
			U.Factory.Application.DependencyManager.DestroyScope<CreatorScope>();
		}
	}
}