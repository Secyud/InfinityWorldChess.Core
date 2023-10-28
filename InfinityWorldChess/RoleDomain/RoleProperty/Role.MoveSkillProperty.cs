#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public FormSkillProperty FormSkill { get; } = new();

        public void AutoEquipFormSkill()
        {
            FormSkill.AutoEquip();
        }

        public class FormSkillProperty : IArchivable
        {
            public SortedDictionary<string, IFormSkill> LearnedSkills { get; } = new();

            private readonly FormSkillContainer[] _skills = new FormSkillContainer[SharedConsts.FormSkillCount];

            public FormSkillContainer this[byte state, byte type] => _skills[GetIndex(state, type)];

            public FormSkillContainer this[int index] => _skills[index];

            public void Set(IFormSkill skill, byte state, byte type)
            {
                _skills[GetIndex(state, type)] = skill is null
                    ? null
                    : new FormSkillContainer(skill, state, type);
            }

            public void Set(IFormSkill skill, int index)
            {
                byte state = (byte)(index / 3);
                byte type = (byte)(index % 3);
                _skills[index] = skill is null
                    ? null
                    : new FormSkillContainer(skill, state, type);
            }

            public void GetGroup(int state, FormSkillContainer[] group)
            {
                for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
                    group[i] = _skills[state * SharedConsts.FormSkillTypeCount + i];
            }

            public static bool CanSet(IFormSkill skill, int index)
            {
                return GetIndex(skill.State, skill.Type) == index;
            }

            public static int GetIndex(byte state, byte type)
            {
                return state * SharedConsts.FormSkillTypeCount + type;
            }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(LearnedSkills.Count);
                foreach (IFormSkill skill in LearnedSkills.Values)
                {
                    writer.WriteObject(skill);
                }

                for (int i = 0; i < SharedConsts.FormSkillCount; i++)
                {
                    writer.Write(_skills[i]?.FormSkill.Name ?? string.Empty);
                }
            }

            public void Load(IArchiveReader reader)
            {
                LearnedSkills.Clear();
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    IFormSkill skill = reader.ReadObject<IFormSkill>();
                    LearnedSkills[skill.Name] = skill;
                }

                for (int i = 0; i < SharedConsts.FormSkillCount; i++)
                {
                    string str = reader.ReadString();
                    Set(str.Length == 0 ? null : LearnedSkills[str], i);
                }
            }

            internal void AutoEquip()
            {
                int[] level = new int[SharedConsts.FormSkillCount];
                IFormSkill[] tmp = new IFormSkill[SharedConsts.FormSkillCount];
                foreach (IFormSkill skill in LearnedSkills.Values)
                {
                    int index = GetIndex(skill.State, skill.Type);
                    if (skill.Score < level[index]) continue;

                    level[index] = skill.Score;
                    tmp[index] = skill;
                }

                for (int i = 0; i < SharedConsts.FormSkillCount; i++)
                    Set(tmp[i], i);
            }
        }
    }
}