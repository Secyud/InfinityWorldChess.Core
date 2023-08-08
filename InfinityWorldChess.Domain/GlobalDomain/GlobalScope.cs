using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.GlobalDomain
{
    public class GlobalScope : DependencyScopeProvider
    {
        private static IMonoContainer<Table> _selectTable;
        private static IMonoContainer<BodyPartSelectComponent> _bodyPartSelect;
        
        public static GlobalScope Instance { get; private set; }
        
        public GlobalScope(IwcAb ab)
        {
            _selectTable ??= MonoContainer<Table>.Create(ab,"SingleSelectTable");
            _bodyPartSelect ??= MonoContainer<BodyPartSelectComponent>.Create(ab);
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