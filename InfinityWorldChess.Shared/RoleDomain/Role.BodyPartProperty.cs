#region

using System;
using Secyud.Ugf.DataManager;

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

		public class BodyPartProperty : DataObject
		{
			[field:S(ID = 0)]
			public RoleBodyPart Living { get; set; } = new();

			[field:S(ID = 1)]
			public RoleBodyPart Kiling { get; set; } = new();

			[field:S(ID = 2)]
			public RoleBodyPart Nimble { get; set; } = new();

			[field:S(ID = 3)]
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