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

        public RegistrableList<string> LastNames { get; } = new();
        public RegistrableList<char> FirstNameFrontFemale { get; } = new();
        public RegistrableList<char> FirstNameFrontMale { get; } = new();
        public RegistrableList<char> FirstNameBehindFemale { get; } = new();
        public RegistrableList<char> FirstNameBehindMale { get; } = new();
        public RegistrableList<string> FirstNamesFemale { get; } = new();
        public RegistrableList<string> FirstNamesMale { get; } = new();

        public RoleResourceManager()
        {
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
                front = FirstNameFrontFemale.Items;
                behind = FirstNameBehindFemale.Items;
            }
            else
            {
                front = FirstNameFrontMale.Items;
                behind = FirstNameBehindMale.Items;
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


        public void RegisterAvatarResourceFromPath(string path, IAssetLoader loader)
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