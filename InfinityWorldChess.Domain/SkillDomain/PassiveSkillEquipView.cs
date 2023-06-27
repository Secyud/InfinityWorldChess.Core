#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using System.Linq;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class PassiveSkillEquipView : SkillEquipView
	{
		private SkillGameContext _context;
		private IPassiveSkill[] _skills;

		private void Awake()
		{
			_context = U.Get<SkillGameContext>();
			_skills = new IPassiveSkill[SharedConsts.PassiveSkillCount];
		}

		private void Refresh()
		{
			for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
				SetSkill(i, TargetRole.PassiveSkill[i]);
		}

		private void SetSkill(int index, IPassiveSkill skill)
		{
			_skills[index] = skill;
			Cells[index].ViewCell.OnInitialize(skill);
		}

		private void SelectRolePassiveSkill(int index, IPassiveSkill skill)
		{
			TargetRole.SetPassiveSkill(skill, index);
			Refresh();
		}

		public override void OnSelect(int index)
		{
		}

		public override void OnHover(int index)
		{
			_skills[index]?.CreateAutoCloseFloatingOnMouse();
		}

		public override void OnInstall(int index)
		{
			_context.OnPassiveSkillSelectionOpen(
				TargetRole.PassiveSkill.LearnedSkills.ToList(),
				skill => SelectRolePassiveSkill(index, skill)
			);
		}

		public override void OnRemove(int index)
		{
			SelectRolePassiveSkill(index, null);
		}

		public override void OnInitialize(Role role)
		{
			base.OnInitialize(role);
			Refresh();
		}
	}
}