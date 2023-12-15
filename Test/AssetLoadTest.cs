using System.Collections.Generic;
using Secyud.Ugf;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace InfinityWorldChess
{
	public class AssetLoadTest : MonoBehaviour
	{
		private void Awake()
		{
			Addressables.LoadAssetsAsync<GameObject>(
			new List<string>
			{
				
				"InfinityWorldChess/GlobalDomain/MainMenuComponent.prefab"
			},U.Log,Addressables.MergeMode.Union);
		}
	}


}

