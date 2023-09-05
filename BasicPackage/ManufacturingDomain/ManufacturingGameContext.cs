#region

using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
    public class ManufacturingGameContext : IRegistry
    {
        public List<ManufacturingButtonDescriptor> ActivityButtons;

        private static string FileName => SharedConsts.SaveFilePath(nameof(ManufacturingGameContext));

        public void OnGameLoading()
        {
            using FileStream stream = File.OpenRead(FileName);
            using DefaultArchiveReader reader = new(stream);

            ActivityButtons = new List<ManufacturingButtonDescriptor>();
            int count = reader.ReadInt32();
            var wc = GameScope.Instance.Map.Value.Grid;
            for (int i = 0; i < count; i++)
            {
                ManufacturingButtonDescriptor tmp = new();
                AddButtonToChecker(tmp, wc.GetCell(reader.ReadInt32()).Get<WorldCell>());
                tmp.Load(reader);
            }
        }

        public void OnGameSaving()
        {
            using FileStream stream = File.OpenWrite(FileName);
            using DefaultArchiveWriter writer = new(stream);
            writer.Write(ActivityButtons.Count);
            foreach (ManufacturingButtonDescriptor b in ActivityButtons)
            {
                writer.Write(b.Target.Cell.Index);
                b.Save(writer);
            }
        }

        public void OnGameCreation()
        {
            ActivityButtons = new List<ManufacturingButtonDescriptor>();

            var worldContext = GameScope.Instance.Map.Value;

            int i = 0;

            foreach (HexCell cell in worldContext.Grid)
            {
                var checker = cell.Get<WorldCell>();
                if (checker.SpecialIndex != 1) continue;

                for (int j = 0; j < 3; j++)
                {
                    ManufacturingButtonDescriptor button = new()
                    {
                        Type = i
                    };
                    AddButtonToChecker(button, checker);
                    i = (i + 1) % 5;
                }
            }
        }

        public void AddButtonToChecker(ManufacturingButtonDescriptor button, WorldCell cell)
        {
            cell.Buttons.Add(button);
            button.Target = cell;
            ActivityButtons.Add(button);
        }
    }
}