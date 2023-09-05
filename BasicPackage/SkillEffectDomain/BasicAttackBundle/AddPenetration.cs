using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillEffectDomain.BasicAttackBundle
{
	
		public class AddPenetration : BasicAttack
		{
			[field: S] public int D256 { get; set; }

			public override string ShowDescription=>base.ShowDescription+
			                                        $"此招式穿透增加{D256}。";

			protected override void PreInteraction(SkillInteraction interaction)
			{
				base.PreInteraction(interaction);
				AttackRecord.Penetration += D256;
			}
		}
}