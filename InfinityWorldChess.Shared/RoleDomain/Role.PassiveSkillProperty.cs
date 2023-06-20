#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role 
	{
		public PassiveSkillProperty PassiveSkill { get; } = new();

		public void SetPassiveSkill(IPassiveSkill passiveSkill, int location)
		{
			PassiveSkill[location, this] = passiveSkill;
		}

		public void AutoEquipPassiveSkill()
		{
			IRoleAiService ai = U.Get<IRoleAiService>();
			if (ai is not null)
				ai.AutoEquipPassiveSkill(this);
			else
				PassiveSkill.AutoSet(this);
		}

		public class PassiveSkillProperty
		{
			private readonly IPassiveSkill[] _skills = new IPassiveSkill[SharedConsts.PassiveSkillCount];

			public List<IPassiveSkill> LearnedSkills { get; } = new();

			public int Yin => _skills.Sum(u => u?.Yin ?? 0);

			public int Yang => _skills.Sum(u => u?.Yang ?? 0);

			public int Living => _skills.Sum(u => u?.Living ?? 0);

			public int Kiling => _skills.Sum(u => u?.Kiling ?? 0);

			public int Nimble => _skills.Sum(u => u?.Nimble ?? 0);

			public int Defend => _skills.Sum(u => u?.Defend ?? 0);

			public int Length => SharedConsts.PassiveSkillCount;

			public IPassiveSkill this[int location] => _skills[location];

			public IPassiveSkill this[int location, Role role]
			{
				set
				{
					if (location >= Length)
						return;

					if (value is not null)
						for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
							if (_skills[i] is not null && _skills[i] == value)
							{
								if (i == location)
									return;

								_skills[i].UnEquip(role);
								_skills[i] = null;
								break;
							}

					IPassiveSkill skill = _skills[location];

					skill?.UnEquip(role);
					value?.Equip(role);

					_skills[location] = value;
				}
			}

			public void Save(BinaryWriter writer)
			{
				writer.Write(LearnedSkills.Count);
				for (int i = 0; i < LearnedSkills.Count; i++)
				{
					IPassiveSkill skill = LearnedSkills[i];
					skill.SaveIndex = i;
					writer.WriteArchiving(skill);
					writer.Write(skill.Yin);
					writer.Write(skill.Yang);
					writer.Write(skill.Living);
					writer.Write(skill.Kiling);
					writer.Write(skill.Nimble);
					writer.Write(skill.Defend);
				}

				for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
					writer.Write(_skills[i]?.SaveIndex ?? -1);
			}

			public void Load(BinaryReader reader, Role role)
			{
				LearnedSkills.Clear();
				int count = reader.ReadInt32();

				for (int i = 0; i < count; i++)
				{
					IPassiveSkill skill = reader.ReadArchiving<IPassiveSkill>();
					skill!.SaveIndex = i;
					skill.Yin = reader.ReadInt16();
					skill.Yang = reader.ReadInt16();
					skill.Living = reader.ReadByte();
					skill.Kiling = reader.ReadByte();
					skill.Nimble = reader.ReadByte();
					skill.Defend = reader.ReadByte();
					LearnedSkills.AddLast(skill);
				}


				for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
				{
					int index = reader.ReadInt32();
					this[i, role] = index < 0 ? null : LearnedSkills[index];
				}
			}

			internal void AutoSet(Role role)
			{
				int minLevel = 0;
				int record = 0;
				IPassiveSkill[] tmp = new IPassiveSkill[SharedConsts.PassiveSkillCount];
				foreach (IPassiveSkill skill in
					LearnedSkills.Where(skill => skill.Score >= minLevel))
				{
					minLevel = skill.Score;
					tmp[record] = skill;
					record = (record + 1) % SharedConsts.PassiveSkillCount;
				}

				for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
					this[i, role] = tmp[i];
			}
		}
	}
}