using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BattleDomain.BattleSkillDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
	
		public class MultiAttackFactor1 : CoreSkillTemplate
		{
			[field: S(ID = 256)] public float F256 { get; set; }

			protected override string HDescription =>
				$"若前置招式为阳，攻击系数乘{1+F256:P0}。";
			
			protected override void PreInteraction(SkillInteraction interaction)
			{
				base.PreInteraction(interaction);
				SkillContainer skill  = BattleScope.Instance.Get<SkillRefreshService>().Skill;
				if ((skill.EquipCode >> skill.EquipLayer - 1 & 1) > 0)
					AttackRecord.AttackFactor *= 1 + F256;
			}
		}
}