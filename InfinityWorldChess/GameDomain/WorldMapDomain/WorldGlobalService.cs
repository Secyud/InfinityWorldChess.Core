#region

using System.Collections.Generic;
using InfinityWorldChess.MapDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
	public class WorldGlobalService :IRegistry
	{
		public readonly PrefabContainer<Transform>[] Features;
		public readonly List<PrefabContainer<Transform>>[,] ResourceFeatures;

		public WorldGlobalService()
		{
			Features = new PrefabContainer<Transform>[SharedConsts.MaxWorldSpecialTypeCount];
			ResourceFeatures =
				new List<PrefabContainer<Transform>>[SharedConsts.MaxWorldResourceTypeCount,
					SharedConsts.MaxWorldResourceLevel];

			for (int i = 0; i < SharedConsts.MaxWorldResourceTypeCount; i++)
			for (int j = 0; j < SharedConsts.MaxWorldResourceLevel; j++)
				ResourceFeatures[i, j] = new List<PrefabContainer<Transform>>();
		}

		public void RegistrarResourceFeatures(int type, int level, params PrefabContainer<Transform>[] features)
		{
			ResourceFeatures[type, level].AddRange(features);
		}

		public void RegistrarResourceFeature(params FeatureDescriptor[] features)
		{
			foreach (FeatureDescriptor feature in features)
				ResourceFeatures[feature.Type, feature.Level].Add(feature.Transform);
		}

		public void RegistrarSpecialFeature(int type, PrefabContainer<Transform> feature)
		{
			Features[type] = feature;
		}
	}
}