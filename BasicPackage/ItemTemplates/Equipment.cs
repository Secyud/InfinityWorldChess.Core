#region

using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public class Equipment : Item, IEquipment
    {
        [field:S(64)]public  List<IInstallable<Role>> Effects { get; } = new();
        [field:S(5)]public byte TypeCode { get; set; }
        [field:S(5)]public byte Location { get; set; }
        [field:S(6)]public int Living { get; set; }
        [field:S(6)]public int Kiling { get; set; }
        [field:S(6)]public int Nimble { get; set; }
        [field:S(6)]public int Defend { get; set; }
        
        public void InstallFrom(Role role)
        {
            foreach (IInstallable<Role> equippable in Effects)
            {
                this.TryAttach(equippable);
                equippable.InstallFrom(role);
            }
        }

        public void UnInstallFrom(Role role)
        {
            foreach (IInstallable<Role> equippable in Effects)
            {
                equippable.UnInstallFrom(role);
            }
        }

        public override void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
            transform.AddEquipmentProperty(this);
            SetEffectContent(transform);
        }
        protected void SetEffectContent(Transform transform)
        {
            transform.AddTitle2("装备效果：");
            
            foreach (IInstallable<Role> equippable in Effects)
            {
                equippable.TrySetContent(transform);
            }
        }

        

        protected void SaveEffects(IArchiveWriter writer)
        {
            writer.Write(Effects.Count);
            foreach (IInstallable<Role> actionable in Effects)
            {
                writer.WriteObject(actionable);
            }
        }

        protected void LoadEffects(IArchiveReader reader)
        {
            Effects.Clear();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                Effects.Add(reader.ReadObject<IInstallable<Role>>());
            }
        }
        
    }
}