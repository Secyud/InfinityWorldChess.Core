using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.CoreSkills
{
	
		public class AddPenetration : CoreSkillTemplate
		{
			[field: S(ID = 256)] public int D256 { get; set; }

			protected override string HDescription =>
				$"此招式穿透增加{D256}。";

			protected override void PreInteraction(SkillInteraction interaction)
			{
				base.PreInteraction(interaction);
				AttackRecord.Penetration += D256;
			}
		}
}