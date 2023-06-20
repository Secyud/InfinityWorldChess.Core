using System.Collections.Generic;
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
			},Debug.Log,Addressables.MergeMode.Union);
		}
	}


}

