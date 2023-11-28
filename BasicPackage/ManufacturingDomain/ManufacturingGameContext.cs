#region

using Secyud.Ugf.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
    public class ManufacturingGameContext : IRegistry
    {
        public List<ManufacturingButtonDescriptor> ActivityButtons;

        private static string FileName => IWCC.SaveFilePath(nameof(ManufacturingGameContext));

        public void OnGameLoading()
        {
            using FileStream stream = File.OpenRead(FileName);
            using DefaultArchiveReader reader = new(stream);

            ActivityButtons = new List<ManufacturingButtonDescriptor>();
            int count = reader.ReadInt32();
            WorldMap map = GameScope.Instance.Map.Value;
            for (int i = 0; i < count; i++)
            {
                ManufacturingButtonDescriptor tmp = new();
                AddButtonToChecker(tmp, map.GetCell(reader.ReadInt32()) as WorldCell);
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
                writer.Write(b.Target.Index);
                b.Save(writer);
            }
        }

        public void OnGameCreation()
        {
            ActivityButtons = new List<ManufacturingButtonDescriptor>();

            WorldMap grid = GameScope.Instance.Map.Value;

            int i = 0;

            foreach (HexCell hexCell in grid.Cells)
            {
                WorldCell cell = (WorldCell)hexCell;

                for (int j = 0; j < 3; j++)
                {
                    ManufacturingButtonDescriptor button = new()
                    {
                        Type = i
                    };
                    AddButtonToChecker(button, cell);
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