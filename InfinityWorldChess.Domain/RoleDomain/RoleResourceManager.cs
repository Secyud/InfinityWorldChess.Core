#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Collections;
using System.Collections.Generic;
using System.IO;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleResourceManager : IRegistry
    {
        public readonly RegistrableDictionary<int, AvatarSpriteContainer>[] MaleAvatarResource =
            new RegistrableDictionary<int, AvatarSpriteContainer>[SharedConsts.AvatarElementCount];

        public readonly RegistrableDictionary<int, AvatarSpriteContainer>[] FemaleAvatarResource =
            new RegistrableDictionary<int, AvatarSpriteContainer>[SharedConsts.AvatarElementCount];

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
            string path = Path.Combine(Application.dataPath, "Data", nameof(RoleResourceManager));
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

            for (int i = 0; i < SharedConsts.AvatarElementCount; i++)
            {
                string atlas = reader.ReadString();

                int count = reader.ReadInt32();

                for (int j = 0; j < count; j++)
                {
                    string assetName = reader.ReadString();
                    SpriteContainer sprite = AtlasSpriteContainer.Create(loader, atlas, assetName);

                    RegistrableDictionary<int, AvatarSpriteContainer> d = 
                        reader.ReadBoolean()
                            ? FemaleAvatarResource[i]
                            : MaleAvatarResource[i];
                    
                    bool normal = reader.ReadBoolean();

                    AvatarElementMessage message = normal ? null :
                            new AvatarElementMessage((AvatarElementType)i,
                                reader.ReadBoolean(),reader.ReadSingle(),reader.ReadSingle(),
                                reader.ReadBoolean(),reader.ReadSingle(),reader.ReadSingle(),
                                reader.ReadBoolean(),reader.ReadSingle(),reader.ReadSingle(),
                                reader.ReadBoolean(),reader.ReadSingle(),reader.ReadSingle());
                    
                    AvatarSpriteContainer container = new(
                        sprite, reader.ReadInt32(),(AvatarElementType)i,message);

                    d.Register(container);
                }
            }
        }
    }
}