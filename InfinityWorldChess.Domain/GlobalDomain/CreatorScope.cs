using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.DependencyInjection;

namespace InfinityWorldChess.GlobalDomain
{
    [DependScope(typeof(GlobalScope))]
    public class CreatorScope : DependencyScope
    {
        private static IMonoContainer<GameCreatorComponent> _gameCreator;
        public static GameCreatorContext Context;
        public CreatorScope(DependencyManager dependencyProvider, IwcAb ab) : base(dependencyProvider)
        {
            _gameCreator ??= MonoContainer<GameCreatorComponent>.Create(ab);

            _gameCreator.Create();
            Context = Get<GameCreatorContext>();
        }

        public override void Dispose()
        {
            _gameCreator.Destroy();
            Context = null;
        }
    }
}