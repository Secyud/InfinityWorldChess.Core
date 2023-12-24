using InfinityWorldChess.ArchivingDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.GlobalDomain
{
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