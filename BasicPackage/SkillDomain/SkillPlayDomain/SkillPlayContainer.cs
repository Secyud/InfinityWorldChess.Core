using System;
using System.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.AssetLoading;
using UnityEngine;

namespace InfinityWorldChess.SkillDomain.SkillPlayDomain
{
    public class SkillPlayContainer : PrefabContainer<SkillPlay>
    {
        public new static AssetContainer<SkillPlay> Create(
            IAssetLoader loader, string assetName)
        {
            return assetName.IsNullOrEmpty()
                ? null
                : new SkillPlayContainer
                {
                    Loader = loader,
                    AssetName = assetName
                };
        }

        public new static AssetContainer<SkillPlay> Create(
            Type loaderType, string assetName)
        {
            return Create(U.Get(loaderType) as IAssetLoader, assetName);
        }

        public new static AssetContainer<SkillPlay> Create<TAssetLoader>(
            string assetName) where TAssetLoader : class, IAssetLoader
        {
            return Create(U.Get<TAssetLoader>(), assetName);
        }

        protected override SkillPlay GetObject()
        {
            return Loader
                .LoadAsset<GameObject>("SkillPlay/" + AssetName + ".prefab")
                .GetComponent<SkillPlay>();
        }


        public override void Save(IArchiveWriter writer)
        {
            writer.WriteNullable(Loader);
            writer.Write(AssetName);
        }

        public override void Load(IArchiveReader reader)
        {
            Loader = reader.ReadNullable<IAssetLoader>();
            AssetName = reader.ReadString();
        }
    }
}