#region

using InfinityWorldChess.SkillDomain;
using System.Collections.Generic;
using InfinityWorldChess.MapDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BattleDomain
{
	public class BattleGlobalService:IRegistry
	{
		public readonly List<PrefabContainer<Transform>>[,] Features;
		public readonly PrefabContainer<Transform>[] SpecialFeatures;

		public BattleGlobalService()
		{
			SpecialFeatures = new PrefabContainer<Transform>[SharedConsts.MaxChessSpecialTypeCount];
			Features =
				new List<PrefabContainer<Transform>>[SharedConsts.MaxChessResourceTypeCount, SharedConsts.MaxChessResourceLevel];

			for (int i = 0; i < SharedConsts.MaxChessResourceTypeCount; i++)
			for (int j = 0; j < SharedConsts.MaxChessResourceLevel; j++)
				Features[i, j] = new List<PrefabContainer<Transform>>();
		}

		public ICoreSkill DefaultCoreSkill { get; set; }

		public IFormSkill DefaultFormSkill { get; set; }

		public void RegistrarResourceFeatures(int type, int level, params PrefabContainer<Transform>[] features)
		{
			Features[type, level].AddRange(features);
		}

		public void RegistrarResourceFeature(params FeatureDescriptor[] features)
		{
			foreach (FeatureDescriptor feature in features)
				Features[feature.Type, feature.Level].Add(feature.Transform);
		}

		public void RegistrarSpecialFeature(int type, PrefabContainer<Transform> feature)
		{
			SpecialFeatures[type] = feature;
		}

		public void CreateBattle(BattleDescriptor battleDescriptor)
		{
			U.M.CreateScope<BattleScope>();
			
			BattleScope.Instance.CreateBattle(battleDescriptor);
		}
		public void DestroyBattle()
		{
			U.M.DestroyScope<BattleScope>();
		}
	}
}