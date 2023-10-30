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
            {
                using FileStream stream = File.OpenRead(SavePath);
                using DefaultArchiveReader reader = new(stream);

                string resourceId = reader.ReadString();
                WorldSetting = U.Tm.ConstructFromResource<WorldSetting>(resourceId);
            }
            
            {
                string path = Path.Combine(WorldSetting.GetDataDirectory(), "map.binary");

                using FileStream stream = File.OpenRead(path);
                using DefaultArchiveReader reader = new(stream);

                Map.Load(reader);

                path = Path.Combine(WorldSetting.GetDataDirectory(), "regions.binary");

                List<WorldCellMessage> messages = U.Tm.ConstructListFromFile<WorldCellMessage>(path);

                foreach (WorldCellMessage message in messages)
                {
                    AddMessage(message);
                    if (U.AddStep())
                        yield return null;
                }
            }
            
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenWrite(SavePath);
            using DefaultArchiveWriter writer = new(stream);

            writer.Write(WorldSetting.ResourceId);
            
            if (U.AddStep())
                yield return null;
        }
        
        public virtual IEnumerator OnGameCreation()
        {
            string settingName = GameCreatorScope.Instance.WorldMessageSetting.WorldName;
            WorldSetting = U.Tm.ConstructFromResource<WorldSetting>(settingName);

            string path = Path.Combine(WorldSetting.GetDataDirectory(), "map.binary");

            using FileStream stream = File.OpenRead(path);
            using DefaultArchiveReader reader = new(stream);

            Map.Load(reader);

            path = Path.Combine(WorldSetting.GetDataDirectory(), "regions.binary");

            List<WorldCellMessage> messages = U.Tm.ConstructListFromFile<WorldCellMessage>(path);

            foreach (WorldCellMessage message in messages)
            {
                AddMessage(message);
            }

            if (U.AddStep())
                yield return null;
        }
    }
}