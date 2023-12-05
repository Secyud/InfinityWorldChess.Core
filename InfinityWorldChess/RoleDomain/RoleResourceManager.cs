#region

using System;
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
            new RegistrableDictionary<int, AvatarSpriteContainer>[IWCC.AvatarElementCount];

        public readonly RegistrableDictionary<int, AvatarSpriteContainer>[] FemaleAvatarResource =
            new RegistrableDictionary<int, AvatarSpriteContainer>[IWCC.AvatarElementCount];

        public List<Tuple<string, Guid>> CoreSkills { get; } = new();
        public List<Tuple<string, Guid>> FormSkills { get; } = new();
        public List<Tuple<string, Guid>> PassiveSkills { get; } = new();
        public List<Tuple<string, Guid>> Items { get; } = new();

        public List<string> LastNames { get; }
        public List<char> FirstNameFrontFemale { get; }
        public List<char> FirstNameFrontMale { get; }
        public List<char> FirstNameBehindFemale { get; }
        public List<char> FirstNameBehindMale { get; }
        public List<string> FirstNamesFemale { get; }
        public List<string> FirstNamesMale { get; }

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
            for (int i = 0; i < IWCC.AvatarElementCount; i++)
            {
                MaleAvatarResource[i] = new RegistrableDictionary<int, AvatarSpriteContainer>();
                FemaleAvatarResource[i] = new RegistrableDictionary<int, AvatarSpriteContainer>();
            }
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
                U.LogWarning($"Avatar resource doesn't exist: {path}");
                return;
            }

            using FileStream stream = File.OpenRead(path);
            using BinaryReader reader = new(stream);

            for (int i = 0; i < IWCC.AvatarElementCount; i++)
            {
                string atlas = reader.ReadString();

                int count = reader.ReadInt32();

                for (int j = 0; j < count; j++)
                {
                    string assetName = reader.ReadString();
                    SpriteContainer sprite = AtlasSpriteContainer.Create(loader, "Portrait/" + atlas, assetName);

                    RegistrableDictionary<int, AvatarSpriteContainer> d =
                        reader.ReadBoolean()
                            ? FemaleAvatarResource[i]
                            : MaleAvatarResource[i];

                    bool normal = reader.ReadBoolean();

                    AvatarElementMessage message = normal
                        ? null
                        : new AvatarElementMessage((AvatarElementType)i,
                            reader.ReadSingle(), reader.ReadSingle(),
                            reader.ReadSingle(), reader.ReadSingle(),
                            reader.ReadSingle(), reader.ReadSingle(),
                            reader.ReadSingle(), reader.ReadSingle());

                    AvatarSpriteContainer container = new(
                        sprite, reader.ReadInt32(), (AvatarElementType)i, message);

                    d.Register(container);
                }
            }
        }
    }
}