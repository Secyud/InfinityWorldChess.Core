#region

using System.Collections;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.HexMap;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexUtilities;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class PlayerGameContext : IRegistry
    {
        private HexUnit _unit;
        public PlayerSetting PlayerSetting { get; } = new();
        public List<IBundle> Bundles { get; } = new();
        public ActivityProperty Activity { get; } = new();

        public Role Role { get; private set; }

        public int SkillPoints { get; set; }

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
            Role = new Role();
            Role.Load(reader, GameScope.Instance.Map.Value.GetCell(index) as WorldCell);

            SkillPoints = reader.ReadInt32();

            PlayerSetting.Load(reader);

            int count = reader.ReadInt32();
            Bundles.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                IBundle bundle = reader.ReadObject<IBundle>();
                bundle.OnGameLoading();
                Bundles.Add(bundle);
                if (U.AddStep())
                {
                    yield return null;
                }
            }

            Activity.Load(reader);
            
            var worldSetting = GameScope.Instance.World.WorldSetting;
            foreach (IBundle bundle in worldSetting.PlayBundles)
            {
                bundle.OnGameLoading();
            }
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveWriter writer = new(stream);
            writer.Write(Role.Position.Index);
            Role.Save(writer);
            writer.Write(SkillPoints);

            PlayerSetting.Save(writer);

            writer.Write(Bundles.Count);
            foreach (IBundle t in Bundles)
            {
                writer.WriteObject(t);
                if (U.AddStep())
                {
                    yield return null;
                }
            }

            Activity.Save(writer);
            
            var worldSetting = GameScope.Instance.World.WorldSetting;
            foreach (IBundle bundle in worldSetting.PlayBundles)
            {
                bundle.OnGameSaving();
            }
        }

        public virtual IEnumerator OnGameCreation()
        {
            GameCreatorScope cs = GameCreatorScope.Instance;

            HexMetrics.InitializeHashGrid(cs.WorldMessageSetting.Seed);

            Role = GameCreatorScope.Instance.Role;

            Bundles.AddRange(cs.Bundles);

            foreach (IBiography biography in cs.Biography)
            {
                biography.OnGameCreation(Role);
            }

            foreach (IBundle bundle in cs.Bundles)
            {
                bundle.OnGameCreation();

                if (U.AddStep())
                {
                    yield return null;
                }
            }
            
            var worldSetting = GameScope.Instance.World.WorldSetting;
            worldSetting.PreparePlayer(this);
            foreach (IBundle bundle in worldSetting.PlayBundles)
            {
                bundle.OnGameCreation();
            }
        }
    }
}