#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using System.Linq;
using InfinityWorldChess.PlayerDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public sealed class CoreSkillEquipView : SkillEquipView
	{
		[SerializeField] private SText[] Selected;
		private SkillGameContext _context;
		private ICoreSkill[] _skills;
		private byte[] _skillSelectArray;

		private void Awake()
		{
			_context = Og.Get<GameScope,SkillGameContext>();
			_skillSelectArray = new byte[SharedConsts.CoreSkillLayerCount];
			_skills = new ICoreSkill[SharedConsts.CoreSkillCount];
			for (int i = 0; i < SharedConsts.CoreSkillLayerCount; i++)
				Selected[i].text = Og.L["阴"];
		}

		private void SelectRoleCoreSkill(int index, ICoreSkill skill)
		{
			int selectLayer = index / SharedConsts.CoreSkillCodeCount;
			byte selectCode = (byte)(index % SharedConsts.CoreSkillCodeCount);

			TargetRole.CoreSkill.Set(skill, selectCode, _skillSelectArray[..selectLayer]);
			RefreshShowSkill(index, skill);
		}

		private void RefreshShowSkills(int layer, byte code)
		{
			if (layer >= SharedConsts.CoreSkillLayerCount - 1) return;

			_skillSelectArray[layer] = code;
			CoreSkillContainer[] skills = new CoreSkillContainer[SharedConsts.CoreSkillCodeCount];
			for (int i = layer; i < SharedConsts.CoreSkillLayerCount; i++)
			{
				TargetRole.CoreSkill.GetGroup(_skillSelectArray[..i], skills);
				for (byte j = 0; j < SharedConsts.CoreSkillCodeCount; j++)
					RefreshShowSkill(i, j, skills[j]);
			}
		}

		private void RefreshShowSkill(int layer, byte code, CoreSkillContainer skill)
		{
			int index = layer * SharedConsts.CoreSkillCodeCount + code;
			RefreshShowSkill(index, skill?.CoreSkill);
		}

		private void RefreshShowSkill(int index, ICoreSkill skill)
		{
			_skills[index] = skill;
			Cells[index].ViewCell.OnInitialize(skill);
		}

		public override void OnSelect(int index)
		{
			int selectLayer = index / SharedConsts.CoreSkillCodeCount;
			byte selectCode = (byte)(index % SharedConsts.CoreSkillCodeCount);
			Selected[selectLayer].text = Og.L[selectCode>0?"阳":"阴"];
			RefreshShowSkills(selectLayer, selectCode);
		}

		public override void OnHover(int index)
		{
			_skills[index]?.CreateAutoCloseFloatingOnMouse();
		}

		public override void OnInstall(int index)
		{
			byte selectLayer = (byte)(index / SharedConsts.CoreSkillCodeCount);
			byte selectCode = Role.CoreSkillProperty.GetCode(
				(byte)(index % SharedConsts.CoreSkillCodeCount), _skillSelectArray[..selectLayer]
			);

			_context.OnCoreSkillSelectionOpen(
				TargetRole.CoreSkill.LearnedSkills
					.Where(u => Role.CoreSkillProperty.CanSet(u, selectLayer, selectCode))
					.ToList(),
				skill => SelectRoleCoreSkill(index, skill)
			);
		}

		public override void OnRemove(int index)
		{
			SelectRoleCoreSkill(index, null);
		}

		public override void OnInitialize(Role role)
		{
			base.OnInitialize(role);
			RefreshShowSkills(0, 0);
		}
	}
}