#region

using InfinityWorldChess.GameDomain.WorldCellDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        [field: S] public RelationProperty Relation { get; } = new();
        [field: S] public int PositionIndex { get; private set; }

        public WorldCell Position
        {
            get => Relation.Position;
            set => Relation.SetPosition(this, value);
        }

        public class RelationProperty
        {
            [S] public float AreaView;
            [S] public float LifeView;

            [S] public float ValueView;
            [S] public float WorldView;

            public WorldCell Position { get; private set; }

            public void SetPosition(Role role, WorldCell to)
            {
                if (to == Position || role is null)
                    return;

                if (Position)
                {
                    Position.InRoles.Remove(role);
                }

                if (to)
                {
                    role.PositionIndex = to.Index;
                    to.InRoles.Add(role);
                }

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