#region

using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	[Registry]
	public interface IRoleAiService
	{
		void AutoEquipCoreSkill(Role role);

		void AutoEquipFormSkill(Role role);

		void AutoEquipPassiveSkill(Role role);

		void AutoEquipEquipment(Role role);
	}
}