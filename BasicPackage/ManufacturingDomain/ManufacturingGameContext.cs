#region

using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
    [Registry]
    public class ManufacturingGameContext
    {
        public List<ManufacturingButtonRegistration> ActivityButtons;

        private static string FileName => SharedConsts.SaveFilePath(nameof(ManufacturingGameContext));

        public void OnGameLoading()
        {
            using FileStream stream = File.OpenRead(FileName);
            using DefaultArchiveReader reader = new(stream);

            ActivityButtons = new List<ManufacturingButtonRegistration>();
            int count = reader.ReadInt32();
            WorldGameContext wc = GameScope.Instance.World;
            for (int i = 0; i < count; i++)
            {
                ManufacturingButtonRegistration tmp = new();
                AddButtonToChecker(tmp, wc.Checkers[reader.ReadInt32()]);
                tmp.Load(reader);
            }
        }

        public void OnGameSaving()
        {
            using FileStream stream = File.OpenWrite(FileName);
            using DefaultArchiveWriter writer = new(stream);
            writer.Write(ActivityButtons.Count);
            foreach (ManufacturingButtonRegistration b in ActivityButtons)
            {
                writer.Write(b.Target.Cell.Index);
                b.Save(writer);
            }
        }

        public void OnGameCreation()
        {
            ActivityButtons = new List<ManufacturingButtonRegistration>();

            WorldGameContext worldContext = GameScope.Instance.World;

            int i = 0;

            foreach (WorldChecker checker in worldContext.Checkers)
            {
                if (checker.SpecialIndex != 1) continue;

                for (int j = 0; j < 3; j++)
                {
                    ManufacturingButtonRegistration button = new ManufacturingButtonRegistration
                    {
                        Type = i
                    };
                    AddButtonToChecker(button, checker);
                    i = (i + 1) % 5;
                }
            }
        }

        public void AddButtonToChecker(ManufacturingButtonRegistration button, WorldChecker checker)
        {
            checker.Buttons.Add(button);
            button.Target = checker;
            ActivityButtons.Add(button);
        }
    }
}