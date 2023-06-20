#region

using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.FunctionalComponents;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#endregion

namespace InfinityWorldChess
{
	public class IwcAb : IAssetLoader
	{
		private static IwcAb _instance;

		public static IwcAb Instance => _instance ??= Og.DefaultProvider.Get<IwcAb>();

		public readonly IObjectAccessor<SFloating> AutoCloseFloating;
		public readonly IObjectAccessor<SText> BodyFieldText;
		public readonly IObjectAccessor<ButtonGroup> ButtonGroupInk;
		public readonly IObjectAccessor<LoadingPanel> LoadingPanelInk;
		public readonly IObjectAccessor<LoadingPanel> LoadingPanelCircle;
		public readonly IObjectAccessor<SFloating> TipFloating;
		public readonly IObjectAccessor<SFloating> EnsureFloating;
		public readonly IObjectAccessor<SText> TitleText1;
		public readonly IObjectAccessor<SText> TitleText2;
		public readonly IObjectAccessor<SText> TitleText3;
		public readonly IObjectAccessor<NormalCell> VerticalCellInk;
		public readonly IObjectAccessor<PropertyRect> PropertyRect;
		public readonly IObjectAccessor<RoleAvatarCell> RoleAvatarCell;
		public readonly IObjectAccessor<SelectOptionCell> SelectOptionCell;

		public IwcAb()
		{
			RoleAvatarCell = PrefabContainer<RoleAvatarCell>.Create(this);
			SelectOptionCell = PrefabContainer<SelectOptionCell>.Create(this);
			AutoCloseFloating = PrefabContainer<SFloating>.Create(this, nameof(AutoCloseFloating));
			VerticalCellInk = PrefabContainer<NormalCell>.Create(this, nameof(VerticalCellInk));
			LoadingPanelInk = PrefabContainer<LoadingPanel>.Create(this, nameof(LoadingPanelInk));
			LoadingPanelCircle = PrefabContainer<LoadingPanel>.Create(this, nameof(LoadingPanelCircle));
			ButtonGroupInk = PrefabContainer<ButtonGroup>.Create(this, nameof(ButtonGroupInk));
			BodyFieldText = PrefabContainer<SText>.Create(this, nameof(BodyFieldText));
			PropertyRect = PrefabContainer<PropertyRect>.Create(this);
			TipFloating = PrefabContainer<SFloating>.Create(this, nameof(TipFloating));
			EnsureFloating = PrefabContainer<SFloating>.Create(this, nameof(EnsureFloating));
			TitleText1 = PrefabContainer<SText>.Create(this, nameof(TitleText1));
			TitleText2 = PrefabContainer<SText>.Create(this, nameof(TitleText2));
			TitleText3 = PrefabContainer<SText>.Create(this, nameof(TitleText3));
		}

		public void Release<TAsset>(TAsset asset) where TAsset : Object
		{
			Addressables.Release(asset);
		}

		public TAsset LoadAsset<TAsset>(string name) where TAsset : Object
		{
			AsyncOperationHandle<TAsset> handle =
				Addressables.LoadAssetAsync<TAsset>(name);
			handle.WaitForCompletion();
#if UNITY_EDITOR
			if (handle.IsValid())
				return handle.Result;
			
			Debug.LogError($"Cannot find resource: {name}");
			
			return null;
#else
			return handle.IsValid() ? handle.Result : null;
#endif
		}
	}
}