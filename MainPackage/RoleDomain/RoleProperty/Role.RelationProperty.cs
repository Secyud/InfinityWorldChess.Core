#region

using System.Runtime.InteropServices;
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

        /// <summary>
        /// 这是简化描述关系用的，暂时没用到，
        /// 一般血缘，人生观，价值观，世界观，地理位置越近，关系就越近。
        /// 这其中可以有很多种组合情况，不需要为每个人单独设置亲密度。
        /// </summary>
        [Guid("D75378C1-D496-F9C7-A5F8-069665EBF390")]
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