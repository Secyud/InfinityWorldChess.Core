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
			writer.Write(flavors.SpicyLevel);
			writer.Write(flavors.SweetLevel);
			writer.Write(flavors.SourLevel);
			writer.Write(flavors.BitterLevel);
			writer.Write(flavors.SaltyLevel);
		}

		public static void LoadFlavors(this IHasFlavor flavors, IArchiveReader reader)
		{
			flavors.SpicyLevel = reader.ReadInt32();
			flavors.SweetLevel = reader.ReadInt32();
			flavors.SourLevel = reader.ReadInt32();
			flavors.BitterLevel = reader.ReadInt32();
			flavors.SaltyLevel = reader.ReadInt32();
		}

		public static void SaveMouthFeel(this IHasMouthfeel mouthfeel, IArchiveWriter writer)
		{
			writer.Write(mouthfeel.HardLevel);
			writer.Write(mouthfeel.LimpLevel);
			writer.Write(mouthfeel.WeakLevel);
			writer.Write(mouthfeel.OilyLevel);
			writer.Write(mouthfeel.SlipLevel);
			writer.Write(mouthfeel.SoftLevel);
		}

		public static void LoadMouthFeel(this IHasMouthfeel mouthfeel, IArchiveReader reader)
		{
			mouthfeel.HardLevel = reader.ReadInt32();
			mouthfeel.LimpLevel = reader.ReadInt32();
			mouthfeel.WeakLevel = reader.ReadInt32();
			mouthfeel.OilyLevel = reader.ReadInt32();
			mouthfeel.SlipLevel = reader.ReadInt32();
			mouthfeel.SoftLevel = reader.ReadInt32();
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