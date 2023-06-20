#region

using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.HexMap.Utilities;
using Secyud.Ugf.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfinityWorldChess.ActivityDomain;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.PlayerDomain
{
    public class PlayerGameContext : IScoped, IOnGameArchiving
    {
        private readonly WorldGameContext _worldGameContext;
        private HexUnit _unit;

        public readonly Dictionary<string, int> GlobalRecord = new();
        public readonly PlayerSetting PlayerSetting= new();
        public readonly List<IBundle> Bundles= new();
        public readonly List<ActivityResult> CompleteActivity = new();

        public Role Role { get; private set; }

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

        public virtual void OnGameLoading(LoadingContext context)
        {
            BinaryReader reader = context.GetReader(nameof(PlayerGameContext));
            ClassManager tm = Og.ClassManager;

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
                IBundle bundle = tm.Construct(reader) as IBundle;
                bundle!.OnGameLoading(context);
                Bundles.Add(bundle);
            }
            
            count = reader.ReadInt32();
            InitializeManager im = Og.InitializeManager;
            CompleteActivity.Capacity = count;
            for (int i = 0; i < count; i++)
            {
                string name = reader.ReadString();
                ActivityResult result = new();
                result.InitSetting(im.GetResource(typeof(ActivityResult),name));
                CompleteActivity.Add(result);
            }
        }

        public virtual void OnGameSaving(SavingContext context)
        {
            BinaryWriter writer = context.GetWriter(nameof(PlayerGameContext));

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
                writer.Write(t.GetTypeId());
            
            writer.Write(CompleteActivity.Count);
            foreach (ActivityResult activityResult in CompleteActivity)
                writer.Write(activityResult.ShowName);
        }

        public virtual void OnGameCreation()
        {
            GameCreatorContext gcc = CreatorScope.Context;

            Role = new Role
            {
                Basic =
                {
                    FirstName = gcc.Basic.FirstName,
                    LastName = gcc.Basic.LastName,
                    BirthHour = gcc.Basic.BirthHour,
                    BirthDay = gcc.Basic.BirthDay,
                    BirthMonth = gcc.Basic.BirthMonth,
                    BirthYear = gcc.Basic.BirthYear,
                    Avatar =
                    {
                        BackItem = gcc.Basic.Avatar.BackItem,
                        BackHair = gcc.Basic.Avatar.BackHair,
                        Body = gcc.Basic.Avatar.Body,
                        Head = gcc.Basic.Avatar.Head,
                        HeadFeature = gcc.Basic.Avatar.HeadFeature,
                        NoseMouth = gcc.Basic.Avatar.NoseMouth,
                        Eye = gcc.Basic.Avatar.Eye,
                        Brow = gcc.Basic.Avatar.Brow,
                        FrontHair = gcc.Basic.Avatar.FrontHair,
                    },
                },
                Nature =
                {
                    Recognize = gcc.Nature.Recognize,
                    Stability = gcc.Nature.Stability,
                    Confident = gcc.Nature.Confident,
                    Efficient = gcc.Nature.Efficient,
                    Gregarious = gcc.Nature.Gregarious,
                    Altruistic = gcc.Nature.Altruistic,
                    Rationality = gcc.Nature.Rationality,
                    Foresighted = gcc.Nature.Foresighted,
                    Intelligent = gcc.Nature.Intelligent,
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

            foreach (IItem item in gcc.Item)
                Role.Item.Add(item);

            HexMetrics.InitializeHashGrid(gcc.WorldSetting.Seed);

            Bundles.AddRange(gcc.Bundles);

            foreach (IBiography biography in gcc.Biography)
                biography.OnGameCreation(Role);

            Role.Position = _worldGameContext.Checkers.First(u => u.SpecialIndex == 1);

            foreach (IBundle bundle in gcc.Bundles)
                bundle.OnGameCreation();
        }
    }
}