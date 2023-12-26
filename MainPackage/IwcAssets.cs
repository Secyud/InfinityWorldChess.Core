#region

using System.Runtime.InteropServices;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.FunctionalComponents;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#endregion

namespace InfinityWorldChess
{
    [Guid("11E30167-E0BD-61B9-8F41-46776AF49A11")]
	public class IwcAssets : IAssetLoader
	{
		public static IwcAssets Instance => _instance ??= U.Get<IwcAssets>();
		private static IwcAssets _instance;

		public readonly IObjectAccessor<SPopup> AutoCloseFloating;
		public readonly IObjectAccessor<SText> BodyFieldText;
		public readonly IObjectAccessor<SButtonGroup> ButtonGroupInk;
		public readonly IObjectAccessor<LoadingPanel> LoadingPanelInk;
		public readonly IObjectAccessor<LoadingPanel> LoadingPanelCircle;
		public readonly IObjectAccessor<SPopup> TipFloating;
		public readonly IObjectAccessor<SPopup> EnsureFloating;
		public readonly IObjectAccessor<SText> TitleText1;
		public readonly IObjectAccessor<SText> TitleText2;
		public readonly IObjectAccessor<SText> TitleText3;
		public readonly IObjectAccessor<PropertyRect> PropertyRect;
		public readonly IObjectAccessor<AvatarEditor> RoleAvatarCell;
		public readonly IObjectAccessor<SelectOptionCell> SelectOptionCell;

		public IwcAssets()
		{
			RoleAvatarCell = PrefabContainer<AvatarEditor>.Create(this);
			SelectOptionCell = PrefabContainer<SelectOptionCell>.Create(this);
			AutoCloseFloating = PrefabContainer<SPopup>.Create(this, nameof(AutoCloseFloating));
			LoadingPanelInk = PrefabContainer<LoadingPanel>.Create(this, nameof(LoadingPanelInk));
			LoadingPanelCircle = PrefabContainer<LoadingPanel>.Create(this, nameof(LoadingPanelCircle));
			ButtonGroupInk = PrefabContainer<SButtonGroup>.Create(this, nameof(ButtonGroupInk));
			BodyFieldText = PrefabContainer<SText>.Create(this, nameof(BodyFieldText));
			PropertyRect = PrefabContainer<PropertyRect>.Create(this);
			TipFloating = PrefabContainer<SPopup>.Create(this, nameof(TipFloating));
			EnsureFloating = PrefabContainer<SPopup>.Create(this, nameof(EnsureFloating));
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
			
			U.LogError($"Cannot find resource: {name}");
			
			return null;
#else
			return handle.IsValid() ? handle.Result : null;
#endif
		}
	}
}