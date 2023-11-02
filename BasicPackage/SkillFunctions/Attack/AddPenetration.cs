using InfinityWorldChess.SkillDomain.SkillInteractionDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.SkillFunctions.Attack
{
	
		public class AddPenetration : BasicAttack
		{
			[field: S] public int Penetration { get; set; }

			public override string Description=>base.Description+
			                                        $"此招式穿透增加{Penetration}。";

			protected override void PreInteraction(SkillInteraction interaction)
			{
				base.PreInteraction(interaction);
				AttackRecord.Penetration += Penetration;
			}
		}
}