#region

using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf.Archiving;

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