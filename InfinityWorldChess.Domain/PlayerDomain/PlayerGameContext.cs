#region

using System.Collections;
using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    [Registry(LifeTime = DependencyLifeTime.Scoped,DependScope = typeof(GameScope))]
    public class PlayerGameContext 
    {
        private readonly WorldGameContext _worldGameContext;
        private HexUnit _unit;

        public readonly Dictionary<string, int> GlobalRecord = new();
        public readonly PlayerSetting PlayerSetting= new();
        public readonly List<IBundle> Bundles= new();
        public readonly List<ActivityResult> CompleteActivity = new();

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

        public PlayerGameContext(WorldGameContext worldGameContext)
        {
            _worldGameContext = worldGameContext;
        }

        public virtual IEnumerator OnGameLoading()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveReader reader = new(stream);

            int index = reader.ReadInt32();
            Role = new Role();
            Role.Load(reader, _worldGameContext.Checkers[index]);

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                GlobalRecord[reader.ReadString()] = reader.ReadInt32();

            PlayerSetting.Load(reader);

            count = reader.ReadInt32();
            Bundles.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                IBundle bundle = reader.Read<IBundle>();
                bundle.OnGameLoading();
                Bundles.Add(bundle);
                if (U.AddStep())
                    yield return null;
            }
            
            count = reader.ReadInt32();
            CompleteActivity.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                string name = reader.ReadString();
                ActivityResult result = DataObject.Create<ActivityResult>(name);
                CompleteActivity.Add(result);
                
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
                writer.Write(t);
            
            writer.Write(CompleteActivity.Count);
            foreach (ActivityResult activityResult in CompleteActivity)
                writer.Write(activityResult.ShowName);
            if (U.AddStep())
                yield return null;
        }

        public virtual IEnumerator OnGameCreation()
        {
            CreatorScope cs = CreatorScope.Instance;

            Role = new Role
            {
                Basic =
                {
                    FirstName = cs.Basic.FirstName,
                    LastName = cs.Basic.LastName,
                    BirthHour = cs.Basic.BirthHour,
                    BirthDay = cs.Basic.BirthDay,
                    BirthMonth = cs.Basic.BirthMonth,
                    BirthYear = cs.Basic.BirthYear,
                    Avatar =
                    {
                        BackItem = cs.Basic.Avatar.BackItem,
                        BackHair = cs.Basic.Avatar.BackHair,
                        Body = cs.Basic.Avatar.Body,
                        Head = cs.Basic.Avatar.Head,
                        HeadFeature = cs.Basic.Avatar.HeadFeature,
                        NoseMouth = cs.Basic.Avatar.NoseMouth,
                        Eye = cs.Basic.Avatar.Eye,
                        Brow = cs.Basic.Avatar.Brow,
                        FrontHair = cs.Basic.Avatar.FrontHair,
                    },
                },
                Nature =
                {
                    Recognize = cs.Nature.Recognize,
                    Stability = cs.Nature.Stability,
                    Confident = cs.Nature.Confident,
                    Efficient = cs.Nature.Efficient,
                    Gregarious = cs.Nature.Gregarious,
                    Altruistic = cs.Nature.Altruistic,
                    Rationality = cs.Nature.Rationality,
                    Foresighted = cs.Nature.Foresighted,
                    Intelligent = cs.Nature.Intelligent,
                },
                BodyPart =
                {
                    Living =
                    {
                        MaxValue = 10,
                        RealValue = 10
                    },
                    Kiling =
                    {
                        MaxValue = 10,
                        RealValue = 10
                    },
                    Nimble =
                    {
                        MaxValue = 10,
                        RealValue = 10
                    },
                    Defend =
                    {
                        MaxValue = 10,
                        RealValue = 10
                    }
                }
            };

            foreach (IItem item in cs.Item)
                Role.Item.Add(item);

            HexMetrics.InitializeHashGrid(cs.WorldSetting.Seed);

            Bundles.AddRange(cs.Bundles);

            foreach (IBiography biography in cs.Biography)
                biography.OnGameCreation(Role);

            Role.Position = _worldGameContext.Checkers.First(u => u.SpecialIndex == 1);

            foreach (IBundle bundle in cs.Bundles)
            {
                bundle.OnGameCreation();
                
                if (U.AddStep())
                    yield return null;
            }
        }
    }
}