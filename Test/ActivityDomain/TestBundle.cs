#region

using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Modularity;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ActivityDomain
{
	public class TestBundle : IBundle
	{
		public string ShowName => "测试天书策";

		public string ShowDescription => "用于测试天书测使用";

		public IObjectAccessor<Sprite> ShowIcon => SpriteContainer.Create<IwcAb>("dragon");

		public void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}


		public void OnGameLoading(LoadingContext context)
		{
		}

		public void OnGameSaving(SavingContext context)
		{
		}

		public void OnGameCreation()
		{
		}
	}
}