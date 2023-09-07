#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
    public static class BpArchiveExtension
    {
        public static void SaveFlavors(this IHasFlavor flavors, IArchiveWriter writer)
        {
            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            {
                writer.Write(flavors.FlavorLevel[i]);
            }
        }

        public static void LoadFlavors(this IHasFlavor flavors, IArchiveReader reader)
        {
            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            {
                flavors.FlavorLevel[i] = reader.ReadInt32();
            }
        }

        public static void SaveMouthFeel(this IHasMouthfeel mouthfeel, IArchiveWriter writer)
        {
            for (int i = 0; i < BasicConsts.MouthFeelCount; i++)
            {
                writer.Write(mouthfeel.MouthFeelLevel[i]);
            }
        }

        public static void LoadMouthFeel(this IHasMouthfeel mouthfeel, IArchiveReader reader)
        {
            for (int i = 0; i < BasicConsts.MouthFeelCount; i++)
            {
                mouthfeel.MouthFeelLevel[i] = reader.ReadInt32();
            }
        }

        public static void SaveShown(this IArchivableShown item, IArchiveWriter writer)
        {
            writer.Write(item.Name ?? string.Empty);
            writer.Write(item.Description ?? string.Empty);
            writer.WriteNullable(item.Icon);
        }

        public static void LoadShown(this IArchivableShown item, IArchiveReader reader)
        {
            item.Name = reader.ReadString();
            item.Description = reader.ReadString();
            item.Icon = reader.ReadNullable<IObjectAccessor<Sprite>>();
        }

        public static void SaveByName(this IArchivableShown shown, IArchiveWriter writer)
        {
            writer.Write(shown.Name);
        }
        public static void LoadByName(this IArchivableShown shown, IArchiveReader reader)
        {
            string name = reader.ReadString();
            TypeDescriptor property = U.Tm.GetProperty(shown.GetType());
            if (property.Resources.TryGetValue(name, out ResourceDescriptor resource))
            {
                resource.WriteToObject(shown);
            }
            else
            {
                Debug.LogError($"Cannot get item from resource. Type: {shown.GetType()}, Name: {name}.");
            }
        }

        public static void SaveSkill(this ISkill skill, IArchiveWriter writer)
        {
            writer.Write(skill.Living);
            writer.Write(skill.Kiling);
            writer.Write(skill.Nimble);
            writer.Write(skill.Defend);
        }
        public static void LoadSkill(this ISkill skill, IArchiveReader reader)
        {
            skill.Living = reader.ReadByte();
            skill.Kiling = reader.ReadByte();
            skill.Nimble = reader.ReadByte();
            skill.Defend = reader.ReadByte();
        }
    }
}