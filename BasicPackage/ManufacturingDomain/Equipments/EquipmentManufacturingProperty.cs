#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public sealed class EquipmentManufacturingProperty : IRoleProperty, IArchivable, ILimitable
    {
        public List<EquipmentProcess> LearnedProcesses { get; } = new();
        public List<EquipmentBlueprint> LearnedBlueprints { get; } = new();

        public void Save(IArchiveWriter writer)
        {
            writer.Write(LearnedProcesses.Count);
            foreach (EquipmentProcess process in LearnedProcesses)
            {
                writer.WriteObject(process);
            }

            writer.Write(LearnedBlueprints.Count);
            foreach (EquipmentBlueprint blueprint in LearnedBlueprints)
            {
                writer.WriteObject(blueprint);
            }
        }

        public void Load(IArchiveReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                LearnedProcesses.Add(reader.ReadObject<EquipmentProcess>());
            }

            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                LearnedBlueprints.Add(reader.ReadObject<EquipmentBlueprint>());
            }
        }

        public bool CheckUseful()
        {
            return LearnedProcesses.Any() || LearnedBlueprints.Any();
        }

        public void InstallFrom(Role target)
        {
        }

        public void UnInstallFrom(Role target)
        {
        }

        public void Overlay(IOverlayable<Role> otherOverlayable)
        {
        }

        public Type Id => typeof(EquipmentManufacturingProperty);
    }
}