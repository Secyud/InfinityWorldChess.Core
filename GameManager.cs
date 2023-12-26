#region

using System;
using System.IO;
using InfinityWorldChess;
using InfinityWorldChess.MainMenuDomain;
using Secyud.Ugf;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.Modularity.Plugins;
using Steamworks;
using UnityEngine;

#endregion

namespace Scenes
{
    public class GameManager : UgfGameManager
    {
        protected override Type StartUpModule => typeof(StartupModule);
        protected override PlugInSourceList PlugInSourceList { get; } = new();


        public override void Awake()
        {
            DontDestroyOnLoad(gameObject);

            PlugInSourceList.AddRange(new IPlugInSource[]
                {
                    new FolderPluginSource(
                        Path.Combine(Application.dataPath, "../Plugins")),
                    new TypePlugInSource(typeof(BasicPackageModule)),
#if !DISABLESTEAMWORKS
                    SteamManager.Instance.PlugInSource,
#endif
                }
            );

            base.Awake();
        }
    }
}