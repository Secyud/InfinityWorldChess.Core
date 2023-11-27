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
    public class Equipment : Item, IEquipment,IAttachProperty
    {
        [field:S(64)]public  List<IEquippable<Role>> Effects { get; } = new();
        [field:S(5)]public byte TypeCode { get; set; }
        [field:S(5)]public byte Location { get; set; }
        [field:S(6)]public byte Living { get; set; }
        [field:S(6)]public byte Kiling { get; set; }
        [field:S(6)]public byte Nimble { get; set; }
        [field:S(6)]public byte Defend { get; set; }
        
        public void Install(Role role)
        {
            foreach (IEquippable<Role> equippable in Effects)
            {
                this.TryAttach(equippable);
                equippable.Install(role);
            }
        }

        public void UnInstall(Role role)
        {
            foreach (IEquippable<Role> equippable in Effects)
            {
                equippable.UnInstall(role);
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
            
            foreach (IEquippable<Role> equippable in Effects)
            {
                equippable.TrySetContent(transform);
            }
        }

        

        protected void SaveEffects(IArchiveWriter writer)
        {
            writer.Write(Effects.Count);
            foreach (IEquippable<Role> actionable in Effects)
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
                Effects.Add(reader.ReadObject<IEquippable<Role>>());
            }
        }
        
    }
}