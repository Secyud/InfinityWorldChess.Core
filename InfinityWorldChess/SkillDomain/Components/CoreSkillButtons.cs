#region

using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class CoreSkillButtons : ButtonRegeditBase<ICoreSkill>
	{
		public CoreSkillButtons()
		{
			Register(new CoreSkillPointDivisionButton());
		}
	}
}