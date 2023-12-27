#region

using System;
using System.IO;
using InfinityWorldChess;
using Secyud.Ugf;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.Modularity.Plugins;
using UnityEngine;

#endregion

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
                    // 一个插件目录来源
                    Path.Combine(Application.dataPath, "../Plugins")),
                // 本体StartupModule并未依赖IwcBasicPackageModule 所以要用插件的形式载入
                // 这种方式可以制作dlc等内容。
                new TypePlugInSource(typeof(IwcBasicPackageModule)),
#if !DISABLESTEAMWORKS
                SteamManager.Instance.PlugInSource,
#endif
            }
        );

        base.Awake();
    }
}