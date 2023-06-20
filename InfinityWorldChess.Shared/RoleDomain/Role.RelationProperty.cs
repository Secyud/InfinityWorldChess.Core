#region

using InfinityWorldChess.WorldDomain;
using System.IO;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public RelationProperty Relation { get; } = new();

		public WorldChecker Position
		{
			get => Relation.Position;
			set => Relation.SetPosition(this, value);
		}

		public class RelationProperty : RoleProperty
		{
			public float AreaView;
			public float LifeView;

			public float ValueView;
			public float WorldView;

			public WorldChecker Position { get; private set; }

			public void SetPosition(Role role, WorldChecker to)
			{
				if (to == Position || role is null)
					return;

				Position?.InRoles.Remove(role);
				to?.InRoles.Add(role);
				Position = to;
			}


			public void Save(BinaryWriter writer)
			{
				writer.Write(AreaView);
				writer.Write(LifeView);
				writer.Write(ValueView);
				writer.Write(WorldView);
			}

			public void Load(BinaryReader reader, Role role, WorldChecker checker)
			{
				SetPosition(role, checker);
				AreaView = reader.ReadSingle();
				LifeView = reader.ReadSingle();
				ValueView = reader.ReadSingle();
				WorldView = reader.ReadSingle();
			}
		}
	}
}