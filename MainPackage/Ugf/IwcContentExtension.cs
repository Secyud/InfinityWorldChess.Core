#region

using Newtonsoft.Json;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class IwcContentExtension
	{
		private static SPopup _tipPopupInstance;
		private static SPopup _ensurePopupInstance;
		private static SPopup _autoClosePopupInstance;
		
		public static RectTransform CreateAutoCloseFloatingOnMouse(this IHasContent hasContent)
		{
			if(_autoClosePopupInstance)
				_autoClosePopupInstance.Destroy();
			_autoClosePopupInstance = U.Get<IwcAssets>().AutoCloseFloating.Value
				.InstantiateOnCanvas();
			_autoClosePopupInstance.InitializeOnMouse();
			_autoClosePopupInstance.RefreshContent(hasContent);
			_autoClosePopupInstance.Replace(ref _popupExist);
			return _autoClosePopupInstance.SubLayoutGroupTrigger.RectTransform;
		}

		public static RectTransform CreateTipFloatingOnCenter(this string tips)
		{
			if(_tipPopupInstance)
				_tipPopupInstance.Destroy();
			_tipPopupInstance = U.Get<IwcAssets>().TipFloating.Value
				.InstantiateOnCanvas();
			_tipPopupInstance.InitializeOnCenter();
			RectTransform ret = _tipPopupInstance.PrepareLayout();
			ret.AddTitle1(U.T.Translate(tips) );
			return ret;
		}
		
		public static RectTransform CreateEnsureFloatingOnCenter(this string tips,Action ensureAction)
		{
			if(_ensurePopupInstance)
				_ensurePopupInstance.Destroy();
			_ensurePopupInstance = U.Get<IwcAssets>().EnsureFloating.Value
				.InstantiateOnCanvas();
			_ensurePopupInstance.InitializeOnCenter();
			_ensurePopupInstance.GetComponent<Ensure>().EnsureAction += ensureAction;
			RectTransform ret = _ensurePopupInstance.PrepareLayout();
			ret.AddParagraph(tips);
			return ret;
		}

		private static SPopup _popupExist;
		
		public static List<string> GetStringListFromPath(this string path)
		{
			if (!File.Exists(path)) return new List<string>();

			try
			{
				return JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
			}
			catch (Exception)
			{
				U.LogError($"Deserialize List<string> From Json Failed! GetPath: {path}");
				return new List<string>();
			}
		}

		public static List<char> GetCharListFromPath(this string path)
		{
			return File.Exists(path) ? File.ReadAllText(path).ToList() : new List<char>();
		}
	}
}