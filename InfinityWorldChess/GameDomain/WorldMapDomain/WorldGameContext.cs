#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Generator;
using Secyud.Ugf.UgfHexMap;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class WorldGameContext:IRegistry
    {
        public readonly IObjectAccessor<HexUnit> WorldUnitPrefab;
        public static WorldMap Map => GameScope.Instance.Map.Value;

        private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(WorldGameContext));

        public WorldGameContext(IwcAssets assets)
        {
            WorldUnitPrefab = PrefabContainer<HexUnit>.Create(
                assets, U.TypeToPath<WorldGameContext>() + "Unit.prefab"
            );
        }


        public virtual IEnumerator OnGameLoading()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveReader reader = new(stream);

            Map.Load(reader);
            
            for (int x = 0; x < Map.CellCountX; x++)
            for (int z = 0; z < Map.CellCountZ; z++)
            {
                Map.GetCell(x,z).Get<WorldCell>().Load(reader);
                if (U.AddStep(64))
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenWrite(SavePath);
            using DefaultArchiveWriter writer = new(stream);

            Map.Save(writer);

            for (int x = 0; x < Map.CellCountX; x++)
            for (int z = 0; z < Map.CellCountZ; z++)
            {
                Map.GetCell(x,z).Get<WorldCell>().Save(writer);
                
                if (U.AddStep(64))
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameCreation()
        {
            WorldSetting worldSetting = GameCreatorScope.Instance.WorldSetting;

            int xMax = worldSetting.WorldSizeX * HexMetrics.ChunkSizeX;
            int zMax = worldSetting.WorldSizeZ * HexMetrics.ChunkSizeZ;

            HexMapGenerator generator = U.Get<HexMapGenerator>();

            generator.Parameter = worldSetting;

            generator.GenerateMap(Map, xMax, zMax);

            if (U.AddStep(64))
                yield return null;
            
            GenerateMapMassage();
        }

        private void GenerateMapMassage()
        {
            HashSet<UgfCell> tmp = new();
            int max = Map.CellCountX * Map.CellCountZ;
            while (tmp.Count < 20)
            {
                UgfCell cell = Map.GetCell(U.GetRandom(max)) .Get<UgfCell>() ;
                if (!cell.IsUnderwater && !cell.HasRiver)
                {
                    tmp.Add(cell);
                    cell.Cell.Get<WorldCell>().SpecialIndex = 0;
                }
            }

            while (tmp.Count < 30)
            {
                UgfCell cell = Map.GetCell(U.GetRandom(max)).Get<UgfCell>()  ;
                if (cell.IsUnderwater ||
                    cell.HasRiver ||
                    tmp.Contains(cell))
                    continue;

                tmp.Add(cell);
                cell.Cell.Get<WorldCell>().SpecialIndex = 1;
            }

            const int threshold = 3;

            foreach (UgfCell cell in tmp)
            {
                int fixD = U.GetRandom(6 / threshold);
                for (int i = 0; i < 6; i += threshold)
                {
                    UgfCell cellTmp = cell;
                    while (cellTmp is not null)
                    {
                        int d = fixD + i + U.GetRandom(6 / threshold) % 6;
                        HexDirection direction = HexDirection.Ne;
                        UgfCell neighbour = null;
                        for (int j = 0; j < threshold; j++)
                        {
                            direction = (HexDirection)((d + j) % 6);
                            UgfCell neighbourTmp = cellTmp.GetNeighbor(direction);
                            if (neighbourTmp is not null && !neighbourTmp.IsUnderwater &&
                                !cellTmp.HasRiverThroughEdge(direction) &&
                                Math.Abs(neighbourTmp.Elevation - cellTmp.Elevation) <= 1)
                            {
                                neighbour = neighbourTmp;
                                break;
                            }
                        }

                        if (neighbour is  null)
                            break;

                        if (neighbour.HasRoads)
                        {
                            cellTmp.AddRoad(direction);
                            break;
                        }

                        cellTmp.AddRoad(direction);
                        cellTmp = neighbour;
                    }
                }
            }
        }
    }
}