#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexUtilities;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class WorldGameContext : IRegistry
    {
        public readonly IObjectAccessor<HexUnit> WorldUnitPrefab;
        public static WorldMap Map => GameScope.Instance.Map.Value;

        private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(WorldGameContext));

        private SortedDictionary<int, IWorldCellMessage> WorldSpecialCell { get; } = new();

        public void AddCellMessage(IWorldCellMessage message)
        {
            HexCell cell = GameScope.Instance.Map.Value.GetCell(message.Index);

        }
        
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
                Map.GetCell(x, z).Load(reader);
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
                Map.GetCell(x, z).Save(writer);

                if (U.AddStep(64))
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameCreation()
        {
            Play play = GameScope.Instance.Play;
            
            string path = $"{Application.persistentDataPath}/{play.MapName}";
          
            FileStream stream = File.OpenRead(path);
            DefaultArchiveReader reader = new(stream);
            Map.Load(reader);

            play.MapSetting.SetMap();
            
            if (U.AddStep(64))
                yield return null;
        }
    }
}