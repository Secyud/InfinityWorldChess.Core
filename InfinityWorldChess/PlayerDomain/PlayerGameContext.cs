#region

using System.Collections;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class PlayerGameContext :IRegistry
    {
        private HexUnit _unit;

        public readonly Dictionary<string, int> GlobalRecord = new();
        public readonly PlayerSetting PlayerSetting= new();
        public readonly List<IBundle> Bundles= new();

        public Role Role { get; private set; }

        private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(PlayerGameContext));

        public virtual HexUnit Unit
        {
            get => _unit;
            internal set
            {
                if (_unit == value)
                    return;

                if (_unit)
                    _unit.Die();
                _unit = value;
            }
        }

        public virtual IEnumerator OnGameLoading()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveReader reader = new(stream);

            int index = reader.ReadInt32();
            Role = new Role(true);
            Role.Load(reader, GameScope.Instance.Map.Value.Grid.GetCell(index).Get<WorldCell>());

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                GlobalRecord[reader.ReadString()] = reader.ReadInt32();

            PlayerSetting.Load(reader);

            count = reader.ReadInt32();
            Bundles.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                IBundle bundle = reader.ReadObject<IBundle>();
                bundle.OnGameLoading();
                Bundles.Add(bundle);
                if (U.AddStep())
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveWriter writer = new(stream);
            writer.Write(Role.Position.Cell.Index);
            Role.Save(writer);

            writer.Write(GlobalRecord.Count);

            foreach (KeyValuePair<string, int> i in GlobalRecord)
            {
                writer.Write(i.Key);
                writer.Write(i.Value);
            }

            PlayerSetting.Save(writer);

            writer.Write(Bundles.Count);
            foreach (IBundle t in Bundles)
                writer.WriteObject(t);
            if (U.AddStep())
                yield return null;
        }

        public virtual IEnumerator OnGameCreation()
        {
            GameCreatorScope cs = GameCreatorScope.Instance;

            HexMetrics.InitializeHashGrid(cs.WorldSetting.Seed);
            
            Role = GameCreatorScope.Instance.Role;

            Bundles.AddRange(cs.Bundles);

            foreach (IBiography biography in cs.Biography)
                biography.OnGameCreation(Role);
            
            Role.Position = GameScope.Instance.Map.Value.Grid
                .First(u => u.Get<WorldCell>().SpecialIndex == 1)
                .Get<WorldCell>();

            foreach (IBundle bundle in cs.Bundles)
            {
                bundle.OnGameCreation();
                
                if (U.AddStep())
                    yield return null;
            }
        }
    }
}