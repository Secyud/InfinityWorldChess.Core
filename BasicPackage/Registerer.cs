using System;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.LevelDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.VirtualPath;

namespace InfinityWorldChess
{
    public static class Registerer
    {
        public static void RegisterDefault(IDependencyManager manager)
        {
            RegisterResourceFolder(manager, "Data/Resources");
            RegisterLevelsFolder(manager, "Data/Levels");
            RegisterAvatarFromFolder(manager, "Data/Portrait");


            RoleResourceManager resource = manager.Get<RoleResourceManager>();
            IVirtualPathManager vpManager = manager.Get<IVirtualPathManager>();
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/LastNames.json"))
            {
                resource.LastNames.RegisterList(path.GetStringListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameFrontFemale.binary"))
            {
                resource.FirstNameFrontFemale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameFrontMale.binary"))
            {
                resource.FirstNameFrontMale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameBehindFemale.binary"))
            {
                resource.FirstNameBehindFemale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNameBehindMale.binary"))
            {
                resource.FirstNameBehindMale.RegisterList(path.GetCharListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNamesMale.binary"))
            {
                resource.FirstNamesMale.RegisterList(path.GetStringListFromPath());
            }
            foreach (string path in vpManager.GetFilesSingly("Data/NameResources/FirstNamesFemale.binary"))
            {
                resource.FirstNamesFemale.RegisterList(path.GetStringListFromPath());
            }
        }

        public static void RegisterResourceFolder(IDependencyManager manager, string path)
        {
            TypeManager tm = manager.Get<TypeManager>();
            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        tm.AddResourcesFromStream(stream);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }


        public static void RegisterLevelsFolder(IDependencyManager manager, string path)
        {
            BattleLevelGlobalContext battleLevelGlobalContext = manager.Get<BattleLevelGlobalContext>();
            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        IBattleLevel[] list = stream.ReadResourceObjects<IBattleLevel>().ToArray();
                        battleLevelGlobalContext.LevelList.RegisterList(list);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }


        public static void RegisterAvatarFromFolder(IDependencyManager manager, string path)
        {
            RoleResourceManager resource = manager.Get<RoleResourceManager>();
            TypeManager typeManager = manager.Get<TypeManager>();

            IEnumerable<string> files = manager.Get<IVirtualPathManager>().GetFilesInFolder(path);
            foreach (string file in files)
            {
                if (file.EndsWith(".binary"))
                {
                    try
                    {
                        using FileStream stream = File.OpenRead(file);
                        using DefaultArchiveReader reader = new(stream);
                        Guid id = reader.ReadGuid();
                        TypeDescriptor descriptor = typeManager[id];
                        if (descriptor is null)
                        {
                            continue;
                        }
                        IAssetLoader loader = manager.Get(descriptor.Type) as IAssetLoader;

                        resource.RegisterAvatarResourceFromPath(reader, loader);
                    }
                    catch (Exception e)
                    {
                        U.LogError($"File analyse failed: {file}");
                        U.LogError(e);
                    }
                }
            }
        }
    }
}