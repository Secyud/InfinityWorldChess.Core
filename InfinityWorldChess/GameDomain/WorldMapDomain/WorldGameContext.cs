#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
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


        public WorldSetting WorldSetting { get; private set; }
        
        public SortedDictionary<int, WorldCellMessage> WorldMessage { get; } = new();

        public void AddMessage(WorldCellMessage message)
        {
            WorldMessage[message.Index] = message;
            WorldCell cell = message.Cell;
            if (cell)
            {
                cell.FeaturePrefab = message.FeaturePrefab?.Value;
            }
        }

        public WorldCellMessage GetMessage(int index)
        {
            WorldMessage.TryGetValue(index, out WorldCellMessage msg);
            return msg;
        }

        public void RemoveMessage(WorldCellMessage message)
        {
            if (WorldMessage.TryGetValue(message.Index, out WorldCellMessage msg))
            {
                if (msg == message)
                {
                    WorldCell cell = message.Cell;
                    if (cell)
                    {
                        cell.FeaturePrefab = null;
                    }

                    WorldMessage.Remove(message.Id);
                }
            }
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

            string resourceId = reader.ReadString();
            WorldSetting = U.Tm.ConstructFromResource<WorldSetting>(resourceId);
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
            writer.Write(WorldSetting.ResourceId);
        }

        public virtual IEnumerator OnGameCreation()
        {
            string settingName = GameCreatorScope.Instance.WorldMessageSetting.WorldName;
            string path = $"{Application.persistentDataPath}/{settingName}";
            WorldSetting = U.Tm.ConstructFromResource<WorldSetting>(settingName);
            FileStream stream = File.OpenRead(path);
            DefaultArchiveReader reader = new(stream);
            
            Map.Load(reader);

            WorldSetting.PrepareWorld(this);

            if (U.AddStep(64))
                yield return null;
        }
    }
}