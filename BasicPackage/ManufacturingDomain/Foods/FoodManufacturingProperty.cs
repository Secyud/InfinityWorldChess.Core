#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public sealed class FoodManufacturingProperty : IRoleProperty, IArchivable, ILimitable
    {
        public List<FoodBlueprint> LearnedBlueprints { get; } = new();

        public void Save(IArchiveWriter writer)
        {
            writer.Write(LearnedBlueprints.Count);
            foreach (FoodBlueprint blueprint in LearnedBlueprints)
            {
                writer.WriteObject(blueprint);
            }
        }

        public void Load(IArchiveReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                LearnedBlueprints.Add(reader.ReadObject<FoodBlueprint>());
            }
        }

        public bool CheckUseful()
        {
            return LearnedBlueprints.Any();
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

        public Type Id => typeof(FoodManufacturingProperty);
    }
}