#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpBinaryExtension
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
			writer.Write(item.Name??string.Empty);
			writer.Write(item.Description??string.Empty);
			writer.WriteNullable(item.Icon);
		}

		public static void LoadShown(this IArchivableShown item, IArchiveReader reader)
		{
			item.Name = reader.ReadString();
			item.Description = reader.ReadString();
			item.Icon = reader.ReadNullable<IObjectAccessor<Sprite>>();
		}

		public static void SaveActiveSkill(this IActiveSkill skill, IArchiveWriter writer,bool saveAsset = false)
		{
			if (saveAsset)
			{
				writer.WriteNullable(skill.UnitPlay);
				writer.Write(skill.Score);
			}
		}

		public static void LoadActiveSkill(this IActiveSkill skill, IArchiveReader reader,bool saveAsset = false)
		{
			if (saveAsset)
			{
				skill.UnitPlay = reader.ReadNullable<IObjectAccessor<HexUnitPlay>>();
				skill.Score= reader.ReadByte();
			}
		}
	}
}