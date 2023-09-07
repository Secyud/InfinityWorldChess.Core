#region

using InfinityWorldChess.SkillDomain;
using System.Collections.Generic;
using System.Linq;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.Archiving;

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
            PassiveSkill.AutoSet(this);
        }

        public class PassiveSkillProperty
        {
            private readonly IPassiveSkill[] _skills = new IPassiveSkill[SharedConsts.PassiveSkillCount];

            public SortedDictionary<string, IPassiveSkill> LearnedSkills { get; } = new();
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

            public void Save(IArchiveWriter writer)
            {
                writer.Write(LearnedSkills.Count);
                foreach (IPassiveSkill skill in LearnedSkills.Values)
                {
                    writer.WriteObject(skill);
                }

                for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
                {
                    writer.Write(_skills[i].ShowName ?? string.Empty);
                }
            }

            public void Load(IArchiveReader reader, Role role)
            {
                LearnedSkills.Clear();
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    IPassiveSkill skill = reader.ReadObject<IPassiveSkill>();
                    LearnedSkills[skill.ShowName] = skill;
                }


                for (int i = 0; i < SharedConsts.PassiveSkillCount; i++)
                {
                    string str = reader.ReadString();
                    this[i, role] = str.Length == 0 ? null : LearnedSkills[str];
                }
            }

            internal void AutoSet(Role role)
            {
                int minLevel = 0;
                int record = 0;
                IPassiveSkill[] tmp = new IPassiveSkill[SharedConsts.PassiveSkillCount];
                foreach (IPassiveSkill skill in LearnedSkills.Values
                             .Where(skill => skill.Score >= minLevel))
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