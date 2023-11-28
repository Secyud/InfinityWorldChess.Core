using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.GameCreatorDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class GameCreatorScope : DependencyScopeProvider
    {
        private readonly IMonoContainer<GameCreatorPanel> _gameCreator;

        public GameCreatorContext Context => Get<GameCreatorContext>();
        
        public static GameCreatorScope Instance { get; private set; }
     
        public GameCreatorScope(IwcAssets assets)
        {
            _gameCreator = MonoContainer<GameCreatorPanel>.Create(assets);
        }

        public override void OnInitialize()
        {
            Instance = this;
            _gameCreator.Create();
        }

        public override void Dispose()
        {
            _gameCreator.Destroy();
            Instance = null;
        }
    }
}