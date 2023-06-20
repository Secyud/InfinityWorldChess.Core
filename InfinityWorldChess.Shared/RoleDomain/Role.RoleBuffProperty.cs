#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role
	{
		public RoleBuffProperty Buffs { get; } = new();

		public void OnBattleInitialize(RoleBattleChess chess)
		{
			float maxHealth = PassiveSkill.Living + BodyPart[BodyType.Living].MaxValue;
			float maxEnergy = maxHealth + PassiveSkill.Yin + PassiveSkill.Yang;
			float execution = maxHealth / 128 + 1;
			chess.InitValue(maxHealth, maxEnergy, (int)execution * 2);
			chess.EnergyRecover = maxEnergy / 16;
			chess.ExecutionRecover = execution;
		}

		public TBuff Get<TBuff>() where TBuff : class, IBuff<Role>
		{
			return Buffs.Get<TBuff>();
		}

		public void Add<TBuff>(TBuff buff) where TBuff : class, IBuff<Role>
		{
			Buffs.Install(buff, this);
		}

		public TBuff GetOrAdd<TBuff>() where TBuff : class, IBuff<Role>
		{
			return Buffs.GetOrInstall<TBuff>(this);
		}

		public void Remove<TBuff>() where TBuff : class, IBuff<Role>
		{
			Buffs.UnInstall<TBuff>(this);
		}

		public class RoleBuffProperty : BuffProperty<Role>
		{
			public List<IOnBattleRoleInitialize> BattleInitializes { get; } = new();
		}
	}
}