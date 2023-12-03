#region

using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public RelationProperty Relation { get; } = new();

        public WorldCell Position
        {
            get => Relation.Position;
            set => Relation.SetPosition(this, value);
        }

        public class RelationProperty
        {
            public float AreaView { get; set; }
            public float LifeView { get; set; }

            public float ValueView { get; set; }
            public float WorldView { get; set; }

            public WorldCell Position { get; private set; }

            public void SetPosition(Role role, WorldCell to)
            {
                if (to == Position || role is null)
                    return;

                Position?.InRoles.Remove(role);

                to?.InRoles.Add(role);

                Position = to;
            }


            public void Save(IArchiveWriter writer)
            {
                writer.Write(AreaView);
                writer.Write(LifeView);
                writer.Write(ValueView);
                writer.Write(WorldView);
            }

            public void Load(IArchiveReader reader, Role role, WorldCell cell)
            {
                SetPosition(role, cell);
                AreaView = reader.ReadSingle();
                LifeView = reader.ReadSingle();
                ValueView = reader.ReadSingle();
                WorldView = reader.ReadSingle();
            }
        }
    }
}