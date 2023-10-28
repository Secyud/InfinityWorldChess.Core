#region

using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public CoreSkillProperty CoreSkill { get; } = new();

        public void AutoEquipCoreSkill()
        {
            CoreSkill.AutoEquip();
        }

        public class CoreSkillProperty : IArchivable
        {
            public SortedDictionary<string, ICoreSkill> LearnedSkills { get; } = new();

            private readonly CoreSkillContainer[][] _layers;

            public CoreSkillProperty()
            {
                _layers = new CoreSkillContainer[SharedConsts.CoreSkillLayerCount][];
                uint count = 1u;
                for (int i = 0; i < SharedConsts.CoreSkillLayerCount; i++)
                {
                    count <<= 1;
                    _layers[i] = new CoreSkillContainer[count];
                }
            }

            public CoreSkillContainer Get(byte maxLayer, byte fullCode)
            {
                FormatCode(maxLayer, ref fullCode);
                return _layers[maxLayer][fullCode];
            }

            public void Set(ICoreSkill value, byte maxLayer, byte fullCode)
            {
                FormatCode(maxLayer, ref fullCode);
                _layers[maxLayer][fullCode] = value is null ? null : new CoreSkillContainer(value, maxLayer, fullCode);
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
                    group[i] = _layers[maxLayer][preCode + (i << maxLayer)];
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

            public static bool CanSet(ICoreSkill skill, byte maxLayer, byte fullCode)
            {
                if (maxLayer < skill.MaxLayer)
                    return false;

                byte skillCode = skill.FullCode;

                FormatCode(maxLayer, ref fullCode);
                FormatCode(skill.MaxLayer, ref skillCode);

                fullCode = (byte)(fullCode >> (maxLayer - skill.MaxLayer));

                return fullCode == skillCode;
            }

            private static void FormatCode(byte maxLayer, ref byte code)
            {
                code = (byte)(code % (1u << maxLayer + 1));
            }


            internal void AutoEquip()
            {
                List<ICoreSkill> tmp = new();

                foreach (ICoreSkill skill in LearnedSkills.Values)
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

                foreach (byte i in _layers[3]
                             .Where(c => c is not null)
                             .Select(c => (byte)(c.EquipCode & 0b111)))
                {
                    TrySetFromList(2, i);
                }

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
                writer.Write(LearnedSkills.Count);
                foreach (ICoreSkill skill in LearnedSkills.Values)
                {
                    writer.WriteObject(skill);
                }

                uint count = 1;
                for (int i = 0; i < SharedConsts.CoreSkillLayerCount; i++)
                {
                    CoreSkillContainer[] layer = _layers[i];
                    count <<= 1;
                    for (int j = 0; j < count; j++)
                    {
                        writer.Write(layer[i]?.Skill.Name ?? string.Empty);
                    }
                }
            }

            public void Load(IArchiveReader reader)
            {
                LearnedSkills.Clear();
                int skillCount = reader.ReadInt32();

                for (int i = 0; i < skillCount; i++)
                {
                    ICoreSkill skill = reader.ReadObject<ICoreSkill>();
                    LearnedSkills[skill.Name] = skill;
                }

                uint count = 1;
                for (byte i = 0; i < SharedConsts.CoreSkillLayerCount; i++)
                {
                    CoreSkillContainer[] layer = _layers[i];
                    count <<= 1;
                    for (byte j = 0; j < count; j++)
                    {
                        string str = reader.ReadString();

                        layer[j] = str.Length == 0
                            ? null
                            : new CoreSkillContainer(
                                LearnedSkills[str],
                                i, j);
                    }
                }
            }
        }
    }
}