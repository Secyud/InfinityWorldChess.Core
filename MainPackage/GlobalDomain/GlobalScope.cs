using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.GlobalDomain
{
    /// <summary>
    /// 全局域不是必须的，甚至是应当抛弃的，因为单例模式本身就是全局域，
    /// 但是把一些常用操作放在服务里难以找寻，还是用全局域容纳一下。
    /// </summary>
    public class GlobalScope : DependencyScopeProvider
    {
        private readonly IMonoContainer<Table> _selectTable;
        public static GlobalScope Instance { get; private set; }
        public GlobalScope(IwcAssets assets)
        {
            _selectTable = MonoContainer<Table>.Create(assets,"SelectableTable");
            
        }

        public override void OnInitialize()
        {
            Instance = this;
        }

        public override void Dispose()
        {
            Instance = null;
        }

        public virtual Table OpenSelect()
        {
            _selectTable.Create();
            return _selectTable.Value;
        }
    }
}