#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DataManager;
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

        private static readonly string SavePath = IWCC.SaveFilePath(nameof(WorldGameContext));

        public WorldSetting WorldSetting { get; private set; }

        public SortedDictionary<int, WorldCellMessage> WorldMessage { get; } = new();
        public SortedDictionary<int, WorldCellMessage> WorldIndexById { get; } = new();

        public void AddMessage(WorldCellMessage message)
        {
            WorldIndexById[message.Id] = message;
            WorldCell cell = message.Cell;
            if (cell)
            {
                cell.Message = message;
            }
        }

        public WorldCellMessage GetMessage(int index)
        {
            WorldMessage.TryGetValue(index, out WorldCellMessage msg);
            return msg;
        }

        public WorldCellMessage GetMessageById(int id)
        {
            WorldIndexById.TryGetValue(id, out WorldCellMessage msg);
            return msg;
        }

        public void RemoveMessage(WorldCellMessage message)
        {
            WorldCell cell = message.Cell;
            if (cell)
            {
                cell.Message = null;
            }

            WorldMessage.Remove(message.Id);
        }

        public WorldGameContext(IwcAssets assets)
        {
            WorldUnitPrefab = PrefabContainer<HexUnit>.Create(
                assets, U.TypeToPath<WorldGameContext>() + "Unit.prefab"
            );
        }

        public virtual IEnumerator OnGameLoading()
        {
            using (FileStream stream = File.OpenRead(SavePath))
            using (DefaultArchiveReader reader = new(stream))
            {
                string resourceId = reader.ReadString();
                WorldSetting = U.Tm.ReadObjectFromResource<WorldSetting>(resourceId);
            }

            return LoadWorld();
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
            string settingName = GameCreatorScope.Instance.Context.WorldMessageSetting.WorldName;
            WorldSetting = U.Tm.ReadObjectFromResource<WorldSetting>(settingName);

            return LoadWorld();
        }

        private IEnumerator LoadWorld()
        {
            using (FileStream stream = File.OpenRead(
                       WorldSetting.GetDataDirectory("map.binary")))
            using (DefaultArchiveReader reader = new(stream))
            {
                Map.Load(reader);
            }

            using (FileStream stream = File.OpenRead(
                       WorldSetting.GetDataDirectory("regions.binary")))
            {
                List<WorldCellMessage> messages =
                    stream.ReadResourceObjects<WorldCellMessage>();

                foreach (WorldCellMessage message in messages)
                {
                    AddMessage(message);
                    if (U.AddStep())
                        yield return null;
                }
            }
        }
    }
}