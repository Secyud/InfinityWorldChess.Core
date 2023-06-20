#region

using Newtonsoft.Json;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Generator;
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
		private static SFloating _tipFloatingInstance;
		private static SFloating _ensureFloatingInstance;
		
		public static RectTransform CreateAutoCloseFloatingOnMouse(this IHasContent hasContent)
		{
			SFloating floating = IwcAb.Instance.AutoCloseFloating.Value.CreateOnMouse();
			floating.RefreshContent(hasContent);
			floating.Replace(ref _floatingExist);
			return floating.SubLayoutGroupTrigger.RectTransform;
		}

		public static RectTransform CreateTipFloatingOnCenter(this string tips)
		{
			if(_tipFloatingInstance)
				_tipFloatingInstance.Destroy();
			_tipFloatingInstance = IwcAb.Instance.TipFloating.Value.CreateOnCenter();
			RectTransform ret = _tipFloatingInstance.PrepareLayout();
			ret.AddTitle1(tips);
			return ret;
		}
		
		public static RectTransform CreateEnsureFloatingOnCenter(this string tips,Action ensureAction)
		{
			if(_ensureFloatingInstance)
				_ensureFloatingInstance.Destroy();
			_ensureFloatingInstance = IwcAb.Instance.EnsureFloating.Value.CreateOnCenter();
			_ensureFloatingInstance.GetComponent<Ensure>().EnsureAction += ensureAction;
			RectTransform ret = _ensureFloatingInstance.PrepareLayout();
			ret.AddTitle1(tips);
			return ret;
		}

		private static SFloating _floatingExist;

		public static HexMapGeneratorParameter GetGeneratorParameter(this IHexCell cell, int x, int z)
		{
			HexMapGeneratorParameter parameter = new()
			{
				ElevationMaximum = cell.Elevation + 3,
				ElevationMinimum = cell.Elevation - 2,
				ChunkSizeMax = Math.Max(x, z),
				ChunkSizeMin = Math.Min(x, z),
				RiverPercentage = cell.HasRiver ? 10 : 0,
				LandPercentage = cell.IsUnderwater ? 30 : cell.HasRiver ? 50 : 95,
				ErosionPercentage = cell.TerrainTypeIndex switch
				{
					0 => 5,
					1 => 10,
					2 => 30,
					3 => 50,
					4 => 70,
					_ => 95
				},
				HighTemperature = (cell.Elevation + 1) / 11.0f,
				LowTemperature = (cell.Elevation - 1) / 11.0f
			};

			return parameter;
		}


		public static List<string> GetStringListFromPath(this string path)
		{
			if (!path.EndsWith(".json"))
				path += ".json";
				
			if (!File.Exists(path)) return new List<string>();

			try
			{
				return JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(path));
			}
			catch (Exception)
			{
				Debug.LogError($"Deserialize List<string> From Json Failed! GetPath: {path}");
				return new List<string>();
			}
		}

		public static List<char> GetCharListFromPath(this string path)
		{
			if (!path.EndsWith(".binary"))
				path += ".binary";
			return File.Exists(path) ? File.ReadAllText(path).ToList() : new List<char>();
		}
	}
}