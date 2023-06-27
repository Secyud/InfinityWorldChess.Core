#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using System.Linq;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class FromSkillEquipView : SkillEquipView
	{
		private IFormSkill[] _skills;
		private SkillGameContext _context;

		private void Awake()
		{
			_skills = new IFormSkill[SharedConsts.FormSkillCount];
			_context = U.Get<SkillGameContext>();
		}

		private void Refresh()
		{
			for (int i = 0; i < SharedConsts.FormSkillCount; i++)
				SetSkill(i, TargetRole.FormSkill[i]?.FormSkill);
		}

		private void SetSkill(int index, IFormSkill skill)
		{
			_skills[index] = skill;
			Cells[index].ViewCell.OnInitialize(skill);
		}

		private void SelectRoleFormSkill(int index, IFormSkill skill)
		{
			TargetRole.FormSkill.Set(skill, index);
			SetSkill(index, skill);
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
			_context.OnFormSkillSelectionOpen(
				TargetRole.FormSkill.LearnedSkills
					.Where(u => Role.FormSkillProperty.CanSet(u, index))
					.ToList(),
				skill => SelectRoleFormSkill(index, skill)
			);
		}

		public override void OnRemove(int index)
		{
			SelectRoleFormSkill(index, null);
		}

		public override void OnInitialize(Role role)
		{
			base.OnInitialize(role);
			Refresh();
		}
	}
}