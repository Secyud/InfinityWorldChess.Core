#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using System;
using System.IO;
using Secyud.Ugf.HexMap;
using UnityEngine;
using UnityEngine.Playables;

#endregion

namespace InfinityWorldChess.Ugf
{
	public static class BpBinaryExtension
	{
		public static void SaveFlavors(this IHasFlavor flavors, BinaryWriter writer)
		{
			writer.Write(flavors.SpicyLevel);
			writer.Write(flavors.SweetLevel);
			writer.Write(flavors.SourLevel);
			writer.Write(flavors.BitterLevel);
			writer.Write(flavors.SaltyLevel);
		}

		public static void LoadFlavors(this IHasFlavor flavors, BinaryReader reader)
		{
			flavors.SpicyLevel = reader.ReadInt32();
			flavors.SweetLevel = reader.ReadInt32();
			flavors.SourLevel = reader.ReadInt32();
			flavors.BitterLevel = reader.ReadInt32();
			flavors.SaltyLevel = reader.ReadInt32();
		}

		public static void SaveMouthFeel(this IHasMouthfeel mouthfeel, BinaryWriter writer)
		{
			writer.Write(mouthfeel.HardLevel);
			writer.Write(mouthfeel.LimpLevel);
			writer.Write(mouthfeel.WeakLevel);
			writer.Write(mouthfeel.OilyLevel);
			writer.Write(mouthfeel.SlipLevel);
			writer.Write(mouthfeel.SoftLevel);
		}

		public static void LoadMouthFeel(this IHasMouthfeel mouthfeel, BinaryReader reader)
		{
			mouthfeel.HardLevel = reader.ReadInt32();
			mouthfeel.LimpLevel = reader.ReadInt32();
			mouthfeel.WeakLevel = reader.ReadInt32();
			mouthfeel.OilyLevel = reader.ReadInt32();
			mouthfeel.SlipLevel = reader.ReadInt32();
			mouthfeel.SoftLevel = reader.ReadInt32();
		}

		public static void SaveShown(this IArchivableShown item, BinaryWriter writer)
		{
			writer.Write(item.Name??string.Empty);
			writer.Write(item.Description??string.Empty);
			writer.WriteNullableArchiving(item.Icon);
		}

		public static void LoadShown(this IArchivableShown item, BinaryReader reader)
		{
			item.Name = reader.ReadString();
			item.Description = reader.ReadString();
			item.Icon = reader.ReadNullableArchiving<IObjectAccessor<Sprite>>();
		}

		public static void SaveActiveSkill(this IActiveSkill skill, BinaryWriter writer,bool saveAsset = false)
		{
			if (saveAsset)
			{
				writer.WriteNullableArchiving(skill.UnitPlay);
				writer.Write(skill.Score);
			}
		}

		public static void LoadActiveSkill(this IActiveSkill skill, BinaryReader reader,bool saveAsset = false)
		{
			if (saveAsset)
			{
				skill.UnitPlay = reader.ReadNullableArchiving<IObjectAccessor<HexUnitPlay>>();
				skill.Score= reader.ReadByte();
			}
		}

	}
}