#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using System.Linq;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        [field: S] public CoreSkillProperty CoreSkill { get; } = new();

        public void AutoEquipCoreSkill()
        {
            CoreSkill.AutoEquip();
        }

        public class CoreSkillProperty : IArchivable
        {
            [S] private readonly List<ICoreSkill> _learnedSkills = new();

            private readonly CoreSkillContainer[] _equippedSkills =
                new CoreSkillContainer[SharedConsts.CoreSkillCount];

            public IReadOnlyList<ICoreSkill> GetLearnedSkills()
            {
                return _learnedSkills;
            }

            public bool TryAddLearnedSkill(ICoreSkill skill)
            {
                if (_learnedSkills.Any(u => u.ResourceId == skill.ResourceId))
                {
                    Debug.LogWarning($"{skill.ResourceId} is already exist;");

                    return false;
                }

                _learnedSkills.Add(skill);
                return true;
            }


            public CoreSkillContainer Get(byte maxLayer, byte fullCode)
            {
                return _equippedSkills[GetIndex(maxLayer, fullCode)];
            }

            public void Set(ICoreSkill value, byte maxLayer, byte fullCode)
            {
                _equippedSkills[GetIndex(maxLayer, fullCode)] =
                    value is null ? null : new CoreSkillContainer(value, maxLayer, fullCode);
            }

            public void Set(ICoreSkill skill, byte lastCode, byte[] codeArray)
            {
                byte fullCode = GetCode(lastCode, codeArray);
                Set(skill, (byte)codeArray.Length, fullCode);
            }

            public void GetGroup(byte[] codeArray, CoreSkillContainer[] group)
            {
                byte preCode = GetCode(0, codeArray);
                GetGroup((byte)codeArray.Length, preCode, group);
            }

            public void GetGroup(byte maxLayer, byte preCode, CoreSkillContainer[] group)
            {
                for (uint i = 0; i < SharedConsts.CoreSkillCodeCount; i++)
                {
                    group[i] = _equippedSkills[GetIndex(maxLayer, (byte)(preCode + (i << maxLayer)))];
                }
            }

            /// <summary>
            /// find code throw select array
            /// </summary>
            /// <param name="lastCode">the last select code</param>
            /// <param name="codeArray">the pre select codes</param>
            /// <returns></returns>
            public static byte GetCode(byte lastCode, params byte[] codeArray)
            {
                uint code = lastCode;
                int len = codeArray.Length - 1;
                for (int i = 0; i < codeArray.Length; i++)
                    code = (code << 1) + codeArray[len - i];
                return (byte)code;
            }

            private int GetIndex(byte maxLayer, byte fullCode)
            {
                int bias = 0, layerLen = 1;
                for (int i = 0; i < maxLayer; i++)
                {
                    layerLen *= SharedConsts.CoreSkillCodeCount;
                    bias += layerLen;
                }

                return GetCode(maxLayer, fullCode) + bias;
            }

            public static bool CanSet(ICoreSkill skill, byte maxLayer, byte fullCode)
            {
                if (maxLayer < skill.MaxLayer)
                    return false;

                byte skillCode = skill.FullCode;

                fullCode = GetCode(maxLayer, fullCode);
                skillCode = GetCode(skill.MaxLayer, skillCode);

                fullCode = (byte)(fullCode >> (maxLayer - skill.MaxLayer));

                return fullCode == skillCode;
            }

            private static byte GetCode(byte maxLayer, byte fullCode)
            {
                return (byte)(fullCode % (1u << maxLayer + 1));
            }


            internal void AutoEquip()
            {
                List<ICoreSkill> tmp = new();

                foreach (ICoreSkill skill in _learnedSkills)
                {
                    if (Get(skill.MaxLayer, skill.FullCode) is null)
                    {
                        Set(skill, skill.MaxLayer, skill.FullCode);
                    }

                    if (skill.MaxLayer < 2)
                    {
                        tmp.Add(skill);
                    }
                }


                const int count = SharedConsts.CoreSkillCodeCount * SharedConsts.CoreSkillCodeCount;

                for (byte i = 0; i < count; i++)
                {
                    TrySetFromList(1, i);
                }
                //
                // foreach (byte i in _layers[3]
                //              .Where(c => c is not null)
                //              .Select(c => (byte)(c.EquipCode & 0b111)))
                // {
                //     TrySetFromList(2, i);
                // }

                return;

                void TrySetFromList(byte layer, byte code)
                {
                    if (Get(layer, code) is not null)
                        return;

                    ICoreSkill skill = tmp.FirstOrDefault(u => CanSet(u, layer, code));
                    if (skill is not null)
                    {
                        Set(skill, layer, code);
                    }
                }
            }

            public void Save(IArchiveWriter writer)
            {
                writer.Write(_learnedSkills.Count);
                for (int i = 0; i < _learnedSkills.Count; i++)
                {
                    ICoreSkill skill = _learnedSkills[i];
                    skill.SaveIndex = i;
                    writer.WriteObject(skill);
                }

                for (int i = 0; i < _equippedSkills.Length; i++)
                {
                    CoreSkillContainer skill = _equippedSkills[i];
                    writer.Write(skill?.Skill.SaveIndex ?? -1);
                }

            }

            public void Load(IArchiveReader reader)
            {
                _learnedSkills.Clear();
                int skillCount = reader.ReadInt32();

                for (int i = 0; i < skillCount; i++)
                {
                    ICoreSkill skill = reader.ReadObject<ICoreSkill>();
                    _learnedSkills.Add(skill);
                }

                for (int i = 0; i < _equippedSkills.Length; i++)
                {
                    int index = reader.ReadInt32();

                    if (index < 0)
                    {
                        _equippedSkills[i] = null;
                    }
                    else
                    {
                        int layerLen = SharedConsts.CoreSkillCodeCount, layer = 0, code = i;
                        for (; code - layerLen > 0;)
                        {
                            code -= layerLen;
                            layer++;
                            layerLen *= SharedConsts.CoreSkillCodeCount;
                        }

                        _equippedSkills[i] = new CoreSkillContainer(
                            _learnedSkills[index], (byte)layer, (byte)code);
                    }
                }
            }
        }
    }
}