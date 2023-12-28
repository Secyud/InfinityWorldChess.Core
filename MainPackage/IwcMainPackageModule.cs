#region

using System.Collections;
using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.VirtualPath;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
    /// <summary>
    /// 核心模块 
    /// </summary>
    [DependsOn(
        typeof(UgfCoreModule),
        typeof(UgfHexMapModule),
        typeof(UgfSteamModule)
    )]
    public class IwcMainPackageModule : IUgfModule, IOnPostConfigure,
        IOnPreInitialization, IOnInitialization, IOnPostInitialization, IOnArchiving
    {
        public void Configure(ConfigurationContext context)
        {
            context.Manager.AddAssembly(typeof(IwcMainPackageModule).Assembly);

            // 虚拟路径 映射一下本地化目录 和数据目录 以在将来加载数据和翻译文本。
            IVirtualPathManager virtualPathManager = context.Get<IVirtualPathManager>();
            virtualPathManager.AddDirectory("Data", Path.Combine(U.Path, "Data"));
            virtualPathManager.AddDirectory("Localization", Path.Combine(U.Path, "Localization"));
        }

        public void PostConfigure(ConfigurationContext context)
        {
            // 注册几个按钮，世界地图右击格子增加一个旅行按钮，
            // 几个技能浏览窗格的右击添加点数分配的按钮。
            context.Get<WorldCellButtons>().Register(new TravelButtonDescriptor());
            context.Get<PassiveSkillButtons>().Register(new PassiveSkillPointDivisionButton());
            context.Get<FormSkillButtons>().Register(new FormSkillPointDivisionButton());
            context.Get<CoreSkillButtons>().Register(new CoreSkillPointDivisionButton());
        }

        public IEnumerator OnGamePreInitialization(GameInitializeContext context)
        {
            // 在游戏初始化时应当优先创建游戏域
            U.M.CreateScope<GameScope>();
            yield return null;
        }

        public IEnumerator OnGameInitializing(GameInitializeContext context)
        {
            // 不加载存档时 从 Creator 获取游戏初始信息。
            yield return GameScope.Instance.World.OnGameCreation();
            yield return GameScope.Instance.Role.OnGameCreation();
            yield return GameScope.Instance.Player.OnGameCreation();
        }

        public IEnumerator OnGamePostInitialization(GameInitializeContext context)
        {
            // 尝试回收 Creator Scope，即使没有创建也没关系，它什么都不做。
            U.M.DestroyScope<GameCreatorScope>();
            // 在后边将玩家人物绑定到地图上。
            context.Get<GameScope>().PostInitializeRole();
            yield return null;
        }

        public IEnumerator SaveGame()
        {
            {
                // 保存时先保存描述信息，以显示存档
                using FileStream stream = File.OpenWrite(MainPackageConsts.SaveFilePath("slot"));
                using DefaultArchiveWriter writer = new(stream);
                GameScope.Instance.Player.Role.Basic.Save(writer);
            }
            yield return GameScope.Instance.World.OnGameSaving();
            yield return GameScope.Instance.Role.OnGameSaving();
            yield return GameScope.Instance.Player.OnGameSaving();
        }

        public IEnumerator LoadGame()
        {
            yield return GameScope.Instance.World.OnGameLoading();
            yield return GameScope.Instance.Role.OnGameLoading();
            yield return GameScope.Instance.Player.OnGameLoading();
        }
    }
}