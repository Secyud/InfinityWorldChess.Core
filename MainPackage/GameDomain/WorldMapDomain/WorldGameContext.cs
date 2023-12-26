#region

using System;
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

        private static readonly string SavePath = MainPackageConsts.SaveFilePath(nameof(WorldGameContext));

        public WorldSetting WorldSetting { get; private set; }

        public SortedDictionary<int, WorldCellMessage> WorldMessage { get; } = new();

        public void AddMessage(WorldCellMessage message)
        {
            WorldCell cell = message.Cell;

            if (cell is not null)
            {
                WorldCellMessage pre = GetMessage(message.Id);
                if (pre is not null)
                {
                    pre.Cell.Message = null;
                }

                WorldMessage[message.Id] = message;
                cell.Message = message;
            }
            else
            {
                U.LogError($"Cell for message {message.Name} is invalid!");
            }
        }

        public WorldCellMessage GetMessage(int id)
        {
            WorldMessage.TryGetValue(id, out WorldCellMessage msg);
            return msg;
        }

        public void RemoveMessage(WorldCellMessage message)
        {
            WorldCell cell = message.Cell;
            if (cell is not null)
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
            foreach (string path in WorldSetting.GetDataDirectory("map.binary"))
            {
                try
                {
                    using FileStream stream = File.OpenRead(path);
                    using DefaultArchiveReader reader = new(stream);
                    GameScope.Instance.Map.Load(reader);
                }
                catch (Exception e)
                {
                    U.LogError(e);
                    continue;
                }

                break;
            }

            foreach (string path in WorldSetting.GetDataDirectory("regions.binary"))
            {
                using FileStream stream = File.OpenRead(path);

                foreach (WorldCellMessage message in
                         stream.ReadResourceObjects<WorldCellMessage>())
                {
                    AddMessage(message);
                    if (U.AddStep())
                        yield return null;
                }
            }
        }
    }
}