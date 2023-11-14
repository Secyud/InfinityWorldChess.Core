#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            private readonly List<IFormSkill> _learnedSkills  = new();

            private readonly FormSkillContainer[] _equippedSkills = new FormSkillContainer[SharedConsts.FormSkillCount];

            public FormSkillContainer this[byte state, byte type] => _equippedSkills[GetIndex(state, type)];

            public FormSkillContainer this[int index] => _equippedSkills[index];
           
            public IReadOnlyList<IFormSkill> GetLearnedSkills()
            {
                return _learnedSkills;
            }

            public bool TryAddLearnedSkill(IFormSkill skill)
            {
                if (_learnedSkills.Any(u => u.ResourceId == skill.ResourceId))
                {
                    Debug.LogWarning($"{skill.ResourceId} is already exist;");

                    return false;
                }

                _learnedSkills.Add(skill);
                return true;
            }
            
            public void Set(IFormSkill skill, byte state, byte type)
            {
                _equippedSkills[GetIndex(state, type)] = skill is null
                    ? null
                    : new FormSkillContainer(skill, state, type);
            }

            public void Set(IFormSkill skill, int index)
            {
                byte state = (byte)(index / 3);
                byte type = (byte)(index % 3);
                _equippedSkills[index] = skill is null
                    ? null
                    : new FormSkillContainer(skill, state, type);
            }

            public void GetGroup(int state, FormSkillContainer[] group)
            {
                for (int i = 0; i < SharedConsts.FormSkillTypeCount; i++)
                    group[i] = _equippedSkills[state * SharedConsts.FormSkillTypeCount + i];
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
                writer.Write(_learnedSkills.Count);
                for (int i = 0; i < _learnedSkills.Count; i++)
                {
                    IFormSkill skill = _learnedSkills[i];
                    writer.WriteObject(skill);
                    skill.SaveIndex = i;
                }

                for (int i = 0; i < SharedConsts.FormSkillCount; i++)
                {
                    FormSkillContainer skill = _equippedSkills[i];
                    writer.Write(skill?.FormSkill.SaveIndex??-1);
                }
            }

            public void Load(IArchiveReader reader)
            {
                _learnedSkills.Clear();
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    IFormSkill skill = reader.ReadObject<IFormSkill>();
                    _learnedSkills.Add(skill);
                }

                for (int i = 0; i < SharedConsts.FormSkillCount; i++)
                {
                    int index  = reader.ReadInt32();
                    Set(index < 0 ? null : _learnedSkills[index], i);
                }
            }

            internal void AutoEquip()
            {
                int[] level = new int[SharedConsts.FormSkillCount];
                IFormSkill[] tmp = new IFormSkill[SharedConsts.FormSkillCount];
                foreach (IFormSkill skill in _learnedSkills)
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