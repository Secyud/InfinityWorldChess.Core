#region

using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.TableComponents;
using System.Linq;
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

		private GameCreatorContext _context;
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
			_context ??= CreatorScope.Context;
			Role.BasicProperty basic = _context.Basic;

			NameEditor.OnInitialize(basic);
			BirthEditor.OnInitialize(basic);
			NatureEditor.OnInitialize(_context.Nature);
			AvatarEditor.OnInitialize(basic);

			_bundleMultiSelectTableHelper.OnInitialize(
				ActivityBundleMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				Og.DefaultProvider.Get<IBundleGlobalService>().Bundles.Get().ToList(),
				_context.Bundles
			);

			_biographyMultiSelectTableHelper.OnInitialize(
				BiographyMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				Og.DefaultProvider.Get<IBiographyGlobalService>().Biographies.Get().ToList(),
				_context.Biography
			);

			_itemMultiSelectTableHelper.OnInitialize(
				ItemMultiSelectTable,
				IwcAb.Instance.VerticalCellInk.Value,
				Og.DefaultProvider.Get<IwcItemGlobalService>().List,
				_context.Item
			);

			PlayerSettingEditor.OnInitialize(_context.PlayerSetting);

			WorldSettingEditor.OnInitialize(_context.WorldSetting);
		}

		public void SetGender(bool female)
		{
			_context.Basic.Female = female;
			AvatarEditor.OnInitialize(_context.Basic);
		}

		public void EnterGame()
		{
			if (!NameEditor.Check())
				return;
			
			IwcAb.Instance.LoadingPanelInk.Instantiate();

			UgfApplicationFactory<StartupModule>.GameCreate();
		}

		public void ReturnMainMenu()
		{
			Og.ScopeFactory.CreateScope<MainMenuScope>();
			Og.ScopeFactory.DestroyScope<CreatorScope>();
		}
	}
}