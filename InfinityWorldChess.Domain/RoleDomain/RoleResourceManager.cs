#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Collections;
using System.Collections.Generic;
using System.IO;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	[Registry()]
	public class RoleResourceManager 
	{
		public readonly AvatarResourceGroup Male = new();
		public readonly AvatarResourceGroup Female = new();

		public readonly List<string> CoreSkills = new();
		public readonly List<string> FormSkills = new();
		public readonly List<string> PassiveSkills = new();
		public readonly List<string> Items = new();
		
		public readonly List<string> LastNames;
		public readonly List<char> FirstNameFrontFemale;
		public readonly List<char> FirstNameFrontMale;
		public readonly List<char> FirstNameBehindFemale;
		public readonly List<char> FirstNameBehindMale;
		public readonly List<string> FirstNamesFemale;
		public readonly List<string> FirstNamesMale;

		public RoleResourceManager()
		{
			string path = Path.Combine(Application.dataPath,"Data", nameof(RoleResourceManager));
			LastNames = Path.Combine(path, nameof(LastNames)).GetStringListFromPath();
			FirstNameFrontFemale = Path.Combine(path, nameof(FirstNameFrontFemale)).GetCharListFromPath();
			FirstNameFrontMale = Path.Combine(path, nameof(FirstNameFrontMale)).GetCharListFromPath();
			FirstNameBehindFemale = Path.Combine(path, nameof(FirstNameBehindFemale)).GetCharListFromPath();
			FirstNameBehindMale = Path.Combine(path, nameof(FirstNameBehindMale)).GetCharListFromPath();
			FirstNamesMale = Path.Combine(path, nameof(FirstNamesMale)).GetStringListFromPath();
			FirstNamesFemale = Path.Combine(path, nameof(FirstNamesFemale)).GetStringListFromPath();
		}


		public string GenerateFirstName(bool female)
		{
			List<char> front, behind;
			if (female)
			{
				front = FirstNameFrontFemale;
				behind = FirstNameBehindFemale;
			}
			else
			{
				front = FirstNameFrontMale;
				behind = FirstNameBehindMale;
			}

			int totalLength = front.Count + behind.Count;
			string ret = "";
			int i1 = U.GetRandom(totalLength);
			int i2 = U.GetRandom(totalLength - i1);
			if (i1 < front.Count)
				ret += front[i1];
			if (i2 < behind.Count)
				ret += behind[i2];
			return ret;
		}


		public void RegisterAvatarResourceFromPath(string path, string atlasName, IAssetLoader loader)
		{
			if (!File.Exists(path))
			{
				Debug.LogWarning($"Avatar resource doesn't exist: {path}");
				return;
			}

			using FileStream stream = File.OpenRead(path);
			using BinaryReader reader = new(stream);

			ReadOne(Female.BackHair, Male.BackHair, atlasName);
			ReadOne(Female.BackItem, Male.BackItem, atlasName);
			ReadOne(Female.FrontHair, Male.FrontHair, atlasName);
			ReadOne(Female.HeadFeature, Male.HeadFeature, atlasName);
			ReadOne(Female.Mouth, Male.Mouth, atlasName);
			ReadOne(Female.Nose, Male.Nose, atlasName);
			ReadTwo(Female.Head, Male.Head, atlasName);
			ReadTwo(Female.Brow, Male.Brow, atlasName);
			ReadTwo(Female.Eye, Male.Eye, atlasName);
			ReadOne(Female.Body, Male.Body, atlasName + "_body");

			void ReadOne(
				RegistrableDictionary<int, AvatarSpriteContainer> fd,
				RegistrableDictionary<int, AvatarSpriteContainer> md,
				string atlas)
			{
				int count = reader.ReadInt32();

				for (int i = 0; i < count; i++)
				{
					bool female = reader.ReadBoolean();
					RegistrableDictionary<int, AvatarSpriteContainer> d = female ? fd : md;
					int id = reader.ReadInt32();
					float scale = reader.ReadSingle();
					string assetName = reader.ReadString();
					Vector2 anchor = new(reader.ReadSingle(), reader.ReadSingle());

					SpriteContainer sprite = AtlasSpriteContainer.Create(loader, atlas, assetName);

					AvatarSpriteContainer container = new(sprite, id, anchor, scale);

					d.Register(container);
				}
			}

			void ReadTwo(
				RegistrableDictionary<int, AvatarSpriteContainer2> fd,
				RegistrableDictionary<int, AvatarSpriteContainer2> md,
				string atlas)
			{
				int count = reader.ReadInt32();

				for (int i = 0; i < count; i++)
				{
					bool female = reader.ReadBoolean();
					RegistrableDictionary<int, AvatarSpriteContainer2> d = female ? fd : md;
					int id = reader.ReadInt32();
					float scale = reader.ReadSingle();
					string assetName1 = reader.ReadString();
					Vector2 anchorFirst = new(reader.ReadSingle(), reader.ReadSingle());
					string assetName2 = reader.ReadString();
					Vector2 anchorSecond = new(reader.ReadSingle(), reader.ReadSingle());

					SpriteContainer lft = AtlasSpriteContainer.Create(loader, atlas, assetName1);
					SpriteContainer rht = AtlasSpriteContainer.Create(loader, atlas, assetName2);

					AvatarSpriteContainer2 container = new(
						lft, rht, id,
						anchorFirst, anchorSecond, scale
					);

					d.Register(container);
				}
			}
		}


	}
}