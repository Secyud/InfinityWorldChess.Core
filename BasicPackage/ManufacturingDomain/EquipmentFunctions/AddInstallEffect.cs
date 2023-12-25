using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DataManager;
using UnityEditor;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentFunctions
{
    public class AddInstallEffect:IActionable<CustomEquipment>
    {
        [field:S] public IInstallable<Role> Installable { get; set; }
        public void Invoke(CustomEquipment target)
        {
            target.Effects.Add(Installable);
        }
    }
}