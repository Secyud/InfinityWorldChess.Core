#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		private RoleBuffProperty _buffs ;

		public RoleBuffProperty Buffs => _buffs ??= new RoleBuffProperty(this);

		public void OnBattleInitialize(BattleRole chess)
		{
			float maxHealth = PassiveSkill.Living + BodyPart[BodyType.Living].MaxValue;
			float maxEnergy = maxHealth + PassiveSkill.Yin + PassiveSkill.Yang;
			float execution = maxHealth / 128 + 1;
			chess.InitValue(maxHealth, maxEnergy, (int)execution * 2);
			chess.EnergyRecover = maxEnergy / 16;
			chess.ExecutionRecover = execution;
		}

		public class RoleBuffProperty : BuffProperty<Role>
		{
			public List<IOnBattleRoleInitialize> BattleInitializes { get; } = new();

			public RoleBuffProperty(Role target) 
			{
				Target = target;
			}

			protected override Role Target { get; }
		}
	}
}