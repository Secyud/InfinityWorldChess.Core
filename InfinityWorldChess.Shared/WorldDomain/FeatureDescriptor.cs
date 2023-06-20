#region

using Secyud.Ugf.AssetLoading;
using UnityEngine;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public struct FeatureDescriptor
	{
		public readonly int Type;

		public readonly int Level;

		public readonly PrefabContainer<Transform> Transform;

		public FeatureDescriptor(int type, int level, PrefabContainer<Transform> transform)
		{
			Type = type;
			Level = level;
			Transform = transform;
		}
	}
}