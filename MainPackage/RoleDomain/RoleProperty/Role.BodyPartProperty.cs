#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public BodyPartProperty BodyPart { get; } = new();

		public int GetSpeed()
		{
			int speed = BodyPart[BodyType.Nimble].MaxValue;
			return Math.Max(64 + 256 * speed / (256 + speed), 1);
		}

		[Guid("CC2E89F1-40B1-5F26-560C-6CF66998E710")]
		public class BodyPartProperty 
		{
			public RoleBodyPart Living { get; set; } = new();

			public RoleBodyPart Kiling { get; set; } = new();

			public RoleBodyPart Nimble { get; set; } = new();

			public RoleBodyPart Defend { get; set; } = new();

			public RoleBodyPart this[BodyType part] =>
				part switch
				{
					BodyType.Living => Living,
					BodyType.Kiling => Kiling,
					BodyType.Nimble => Nimble,
					BodyType.Defend => Defend,
					_               => throw new ArgumentOutOfRangeException(nameof(part), part, null)
				};
		}
	}
}