using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.SkillDomain
{
	public class PassiveSkillButtons : ButtonRegeditBase<IPassiveSkill>
	{
		public PassiveSkillButtons()
		{
			Register(new PassiveSkillPointDivisionButton());
		}
	}
}