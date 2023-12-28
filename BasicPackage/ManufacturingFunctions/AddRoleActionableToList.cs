using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.ManufacturingFunctions
{
    public class AddRoleActionable
    {
        [field:S] public IActionable<Role> RoleActionAble { get; set; }

        protected void Add(Consumable consumable)
        {
            consumable.EffectsInWorld.Add(RoleActionAble);
        }
    }
}