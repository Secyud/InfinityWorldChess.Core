#region

using Secyud.Ugf.Archiving;
using System;
using System.IO;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public BodyPartProperty BodyPart { get; } = new();

		public int GetSpeed()
		{
			int speed = BodyPart[BodyType.Nimble].MaxValue;
			return Math.Max((int)(64 + 256 * speed / (256 + speed)), 1);
		}

		public class BodyPartProperty : IArchivable
		{
			public RoleBodyPart Living { get; set; } = new();

			public RoleBodyPart Kiling { get; set; } = new();

			public RoleBodyPart Nimble { get; set; } = new();

			public RoleBodyPart Defend { get; set; } = new();

			public RoleBodyPart this[BodyType part]
			{
				get => part switch
				{
					BodyType.Living => Living,
					BodyType.Kiling => Kiling,
					BodyType.Nimble => Nimble,
					BodyType.Defend => Defend,
					_ => throw new ArgumentOutOfRangeException(nameof(part), part, null)
				};
			}

			public void Save(BinaryWriter writer)
			{
				Living.Save(writer);
				Kiling.Save(writer);
				Nimble.Save(writer);
				Defend.Save(writer);
			}

			public void Load(BinaryReader reader)
			{
				Living.Load(reader);
				Kiling.Load(reader);
				Nimble.Load(reader);
				Defend.Load(reader);
			}
		}
	}
}