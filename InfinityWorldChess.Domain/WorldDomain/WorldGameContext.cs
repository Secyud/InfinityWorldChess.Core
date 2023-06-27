#region

using System;
using System.Collections;
using System.Collections.Generic;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Generator;
using Secyud.Ugf.HexMap.Utilities;
using System.IO;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.WorldDomain
{
    [Registry()]
    public partial class WorldGameContext
    {
        public readonly IObjectAccessor<HexUnit> WorldUnitPrefab;
        public WorldChecker[] Checkers { get; private set; }
        public WorldUiComponent Ui { get; internal set; }
        public WorldMapComponent Map { get; internal set; }

        private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(WorldGameContext));

        public WorldGameContext(IwcAb ab)
        {
            WorldUnitPrefab = PrefabContainer<HexUnit>.Create(
                ab, U.TypeToPath<WorldGameContext>() + "Unit.prefab"
            );
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


        public virtual IEnumerator OnGameLoading()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveReader reader = new(stream);

            Map.Grid.Load(reader);
            
            int cellCount = reader.ReadInt32();
            Checkers = new WorldChecker[cellCount];
            for (int i = 0; i < cellCount; i++)
            {
                Checkers[i] = new WorldChecker(Map.Grid.GetCell(i));
                Checkers[i].Load(reader);
                if (U.AddStep(64))
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenWrite(SavePath);
            using DefaultArchiveWriter writer = new(stream);

            Map.Grid.Save(writer);

            writer.Write(Checkers.Length);
            foreach (WorldChecker checker in Checkers)
            {
                checker.Save(writer);
                
                if (U.AddStep(64))
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameCreation()
        {
            WorldSetting worldSetting = CreatorScope.Instance.WorldSetting;

            int xMax = worldSetting.WorldSizeX * HexMetrics.ChunkSizeX;
            int zMax = worldSetting.WorldSizeZ * HexMetrics.ChunkSizeZ;
            int count = xMax * zMax;


            Checkers = new WorldChecker[count];

            HexMapGenerator generator = U.Get<HexMapGenerator>();

            generator.Parameter = worldSetting;

            generator.GenerateMap(Map.Grid, xMax, zMax);

            for (int i = 0; i < xMax * zMax; i++)
            {
                Checkers[i] = new WorldChecker(Map.Grid.GetCell(i));
                if (U.AddStep(64))
                    yield return null;
            }

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
                int fixD = U.GetRandom(6 / threshold);
                for (int i = 0; i < 6; i += threshold)
                {
                    HexCell cell = checker.Cell;
                    while (cell)
                    {
                        int d = fixD + i + U.GetRandom(6 / threshold) % 6;
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