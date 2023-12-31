#region

using InfinityWorldChess.SkillDomain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Secyud.Ugf;
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

        [Guid("7DC8299E-97AA-A5AD-DEE9-2CE129593C26")]
        public class PassiveSkillProperty
        {
            private readonly List< IPassiveSkill> _learnedSkills = new();
            private readonly IPassiveSkill[] _equippedSkills = new IPassiveSkill[MainPackageConsts.PassiveSkillCount];

            public IReadOnlyList<IPassiveSkill> GetLearnedSkills()
            {
                return _learnedSkills;
            }

            public bool TryAddLearnedSkill(IPassiveSkill skill)
            {
                if (_learnedSkills.Any(u => u.ResourceId == skill.ResourceId))
                {
                    U.LogWarning($"{skill.ResourceId} is already exist;");
                    return false;
                }

                _learnedSkills.Add(skill);
                return true;
            }
            
            public int Living => _equippedSkills.Sum(u => u?.Living ?? 0);

            public int Kiling => _equippedSkills.Sum(u => u?.Kiling ?? 0);

            public int Nimble => _equippedSkills.Sum(u => u?.Nimble ?? 0);

            public int Defend => _equippedSkills.Sum(u => u?.Defend ?? 0);

            public int Length => MainPackageConsts.PassiveSkillCount;

            public IPassiveSkill this[int location] => _equippedSkills[location];

            public IPassiveSkill this[int location, Role role]
            {
                set
                {
                    if (location >= Length)
                        return;

                    if (value is not null)
                        for (int i = 0; i < MainPackageConsts.PassiveSkillCount; i++)
                            if (_equippedSkills[i] is not null && _equippedSkills[i] == value)
                            {
                                if (i == location)
                                    return;

                                _equippedSkills[i].UnInstallFrom(role);
                                _equippedSkills[i] = null;
                                break;
                            }

                    IPassiveSkill skill = _equippedSkills[location];

                    skill?.UnInstallFrom(role);
                    value?.InstallFrom(role);

                    _equippedSkills[location] = value;
                }
            }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(_learnedSkills.Count);
                for (int i = 0; i < _learnedSkills.Count; i++)
                {
                    IPassiveSkill skill = _learnedSkills[i];
                    writer.WriteObject(skill);
                    // 这里要设置索引了，后续引用保存应当使用索引。
                    skill.SaveIndex = i;
                }

                for (int i = 0; i < MainPackageConsts.PassiveSkillCount; i++)
                {
                    // 保存索引而非对象。
                    writer.Write(_equippedSkills[i]?.SaveIndex ?? -1);
                }
            }

            public void Load(IArchiveReader reader, Role role)
            {
                _learnedSkills.Clear();
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    IPassiveSkill skill = reader.ReadObject<IPassiveSkill>();
                    _learnedSkills.Add(skill);
                }


                for (int i = 0; i < MainPackageConsts.PassiveSkillCount; i++)
                {
                    int index = reader.ReadInt32();
                    this[i, role] = index < 0 ? null : _learnedSkills[index];
                }
            }

            internal void AutoSet(Role role)
            {
                int minLevel = 0;
                int record = 0;
                IPassiveSkill[] tmp = new IPassiveSkill[MainPackageConsts.PassiveSkillCount];
                foreach (IPassiveSkill skill in _learnedSkills
                             .Where(skill => skill.Score >= minLevel))
                {
                    minLevel = skill.Score;
                    tmp[record] = skill;
                    record = (record + 1) % MainPackageConsts.PassiveSkillCount;
                }

                for (int i = 0; i < MainPackageConsts.PassiveSkillCount; i++)
                    this[i, role] = tmp[i];
            }
        }
    }
}