#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpSkillExtension
	{
		public static BattleEventsBuff GetBattleEvents(this BattleRole chess)
		{
			return chess.GetOrInstall<BattleEventsBuff>();
		}

		public static AttackRecordBuff SetAttack(this SkillInteraction interaction)
		{
			AttackRecordBuff attackRecord = interaction.GetOrInstall<AttackRecordBuff>();
			if (interaction.LaunchChess is ICanAttack attacker)
				attackRecord.Attack = attacker.AttackValue;
			if (interaction.TargetChess is ICanDefend defender)
				attackRecord.Defend = defender.DefendValue;
			return attackRecord;
		}
		
		public static float RunAttack(this SkillInteraction interaction)
		{
			AttackRecordBuff attack = interaction.Get<AttackRecordBuff>();
			float ret = 0;
			if (attack is not null)
			{
				interaction.BeforeHit();
				if (interaction.TargetChess is ICanDefend defender)
					ret = attack.RunDamage(defender);
				interaction.AfterHit();
			}
			return ret;
		}
		
		
		public static bool FitWeapon(this ICoreSkill coreSkill, BattleRole chess)
		{
			byte tc = chess.Role.Equipment[BodyType.Kiling]?.TypeCode ?? 0;
			return ((tc ^ coreSkill.ConditionCode) & coreSkill.ConditionMask) == 0;
		}
	}
}