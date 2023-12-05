using Secyud.Ugf.TableComponents.ButtonComponents;

namespace InfinityWorldChess.SkillDomain
{
	public class FormSkillButtons: ButtonRegeditBase<IFormSkill> 
	{
		public FormSkillButtons()
		{
			Register(new FormSkillPointDivisionButton());
		}
	}
}