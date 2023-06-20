using InfinityWorldChess.SkillDomain;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
	
		public class AddPenetration : CoreSkillTemplate
		{
			[R(256)] public int D256 { get; set; }

			protected override string HDescription =>
				$"此招式穿透增加{D256}。";

			protected override void PreInteraction(SkillInteraction interaction)
			{
				base.PreInteraction(interaction);
				AttackRecord.Penetration += D256;
			}
		}
}