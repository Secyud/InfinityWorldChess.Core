#region

using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;

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

		public class RelationProperty : RoleProperty
		{
			[S]public float AreaView;
			[S]public float LifeView;

			[S]public float ValueView;
			[S]public float WorldView;

			public WorldCell Position { get; private set; }

			public void SetPosition(Role role, WorldCell to)
			{
				if (to == Position || role is null)
					return;

				Position?.InRoles.Remove(role);
				to?.InRoles.Add(role);
				Position = to;

				if (Position is not null)
				{
					GlobalScope.Instance.RoleContext.AddRole(role);
				}
				else
				{
					GlobalScope.Instance.RoleContext.RemoveRole(role);
				}
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