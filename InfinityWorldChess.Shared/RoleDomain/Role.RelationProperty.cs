#region

using InfinityWorldChess.GameDomain;
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
			[S(ID = 0)]public float AreaView;
			[S(ID = 1)]public float LifeView;

			[S(ID = 2)]public float ValueView;
			[S(ID = 3)]public float WorldView;

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