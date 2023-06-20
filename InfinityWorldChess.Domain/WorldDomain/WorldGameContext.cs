#region

using System;
using System.Collections.Generic;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Generator;
using Secyud.Ugf.HexMap.Utilities;
using Secyud.Ugf.Modularity;
using System.IO;
using UnityEngine.Playables;

#endregion

namespace InfinityWorldChess.WorldDomain
{
	public partial class WorldGameContext : ISingleton, IOnGameArchiving
	{
		public readonly IObjectAccessor<HexUnit> WorldUnitPrefab;
		public readonly IObjectAccessor<PlayableAsset> DefaultRunning;
		public readonly IObjectAccessor<PlayableAsset> DefaultBreathing;
		public WorldChecker[] Checkers { get; private set; }
		public WorldUiComponent Ui { get; internal set; }
		public WorldMapComponent Map { get; internal set; }

		public WorldGameContext(IwcAb ab)
		{
			WorldUnitPrefab = PrefabContainer<HexUnit>.Create(
				ab, Og.TypeToPath<WorldGameContext>() + "Unit.prefab"
			);
			DefaultRunning = AssetContainer<PlayableAsset>.Create(ab, "Playable/Timeline/Running.playable");
			DefaultBreathing = AssetContainer<PlayableAsset>.Create(ab, "Playable/Timeline/Breathing.playable");
		}


		public WorldChecker GetChecker(HexCoordinates coordinates)
		{
			int index = Map.Grid.GetCellIndex(coordinates);
			return index < 0 ? null : Checkers[index];
		}

		public WorldChecker GetChecker(HexCell cell)
		{
			if (!cell) return null;

			return cell.Index < 0 ? null : Checkers[cell.Index];
		}


		public virtual void OnGameLoading(LoadingContext context)
		{
			BinaryReader reader = context.GetReader(nameof(WorldGameContext));

			Map.Grid.Load(reader);

			int cellCount = reader.ReadInt32();
			Checkers = new WorldChecker[cellCount];
			for (int i = 0; i < cellCount; i++)
			{
				Checkers[i] = new WorldChecker(Map.Grid.GetCell(i));
				Checkers[i].Load(reader);
			}
		}

		public virtual void OnGameSaving(SavingContext context)
		{
			BinaryWriter writer = context.GetWriter(nameof(WorldGameContext));

			Map.Grid.Save(writer);

			writer.Write(Checkers.Length);
			foreach (WorldChecker checker in Checkers)
				checker.Save(writer);
		}

		public virtual void OnGameCreation()
		{
			WorldSetting worldSetting = CreatorScope.Context.WorldSetting;

			int xMax = worldSetting.WorldSizeX * HexMetrics.ChunkSizeX;
			int zMax = worldSetting.WorldSizeZ * HexMetrics.ChunkSizeZ;
			int count = xMax * zMax;


			Checkers = new WorldChecker[count];

			HexMapGenerator generator = Og.DefaultProvider.Get<HexMapGenerator>();

			generator.Parameter = worldSetting;

			generator.GenerateMap(Map.Grid, xMax, zMax);

			for (int i = 0; i < xMax * zMax; i++)
				Checkers[i] = new WorldChecker(Map.Grid.GetCell(i));

			GenerateMapMassage();
		}

		private void GenerateMapMassage()
		{
			HashSet<WorldChecker> tmp = new();
			while (tmp.Count < 20)
			{
				WorldChecker checker = Checkers.RandomPick();
				if (!checker.Cell.IsUnderwater && !checker.Cell.HasRiver)
				{
					tmp.Add(checker);
					checker.SpecialIndex = 0;
				}
			}

			while (tmp.Count < 30)
			{
				WorldChecker checker = Checkers.RandomPick();
				if (checker.Cell.IsUnderwater ||
				    checker.Cell.HasRiver ||
				    tmp.Contains(checker))
					continue;

				tmp.Add(checker);
				checker.SpecialIndex = 1;
			}

			const int threshold = 3;

			foreach (WorldChecker checker in tmp)
			{
				int fixD = Og.GetRandom(6 / threshold);
				for (int i = 0; i < 6; i += threshold)
				{
					HexCell cell = checker.Cell;
					while (cell)
					{
						int d = fixD + i + Og.GetRandom(6 / threshold) % 6;
						HexDirection direction = HexDirection.Ne;
						HexCell neighbour = null;
						for (int j = 0; j < threshold; j++)
						{
							direction = (HexDirection)((d + j) % 6);
							HexCell neighbourTmp = cell.GetNeighbor(direction);
							if (neighbourTmp && !neighbourTmp.IsUnderwater &&
							    !cell.HasRiverThroughEdge(direction) &&
							    Math.Abs(neighbourTmp.Elevation - cell.Elevation) <= 1)
							{
								neighbour = neighbourTmp;
								break;
							}
						}

						if (!neighbour)
							break;

						if (neighbour.HasRoads)
						{
							cell.AddRoad(direction);
							break;
						}
						cell.AddRoad(direction);
						cell = neighbour;
					}
				}
			}
		}
	}
}