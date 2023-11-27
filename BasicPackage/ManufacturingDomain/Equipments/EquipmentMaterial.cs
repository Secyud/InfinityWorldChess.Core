#region

using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentMaterial : Item,IAttachProperty
    {
        [field:S(6)]public byte Living { get; set; }
        [field:S(6)]public byte Kiling { get; set; }
        [field:S(6)]public byte Nimble { get; set; }
        [field:S(6)]public byte Defend { get; set; }
        [field:S(7)]public byte Length { get; set; }
        [field:S(32)]public IActionable<CustomEquipment> StartEffects { get; set; }
        [field:S(32)]public IActionable<CustomEquipment> FinishEffects { get; set; }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddShapeProperty(this);
            transform.AddParagraph($"拥有格数{Length}");
            StartEffects?.TrySetContent(transform);
            FinishEffects?.TrySetContent(transform);
        }

        public void StartEquipmentManufacturing(CustomEquipment equipment)
        {
            this.TryAttach(StartEffects);
            StartEffects?.Invoke(equipment);
        }
        public void FinishEquipmentManufacturing(CustomEquipment equipment)
        {
            this.TryAttach(FinishEffects);
            FinishEffects?.Invoke(equipment);
        }
        
        
    }
}