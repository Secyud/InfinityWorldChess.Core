#region

using Secyud.Ugf;
using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role :  IHasContent
    {
        [field:S]public int Id { get; private set; }

        private readonly Dictionary<Type, RoleProperty> _extraProperties = new();
        private HexUnit _unit;

        public Role(bool load = false)
        {
            if (!load)
            {
                Id = GlobalScope.Instance.RoleContext.GetNewId();
            }
        }

        public void SetContent(Transform transform)
        {
            transform.AddTitle1(ShowName);
            transform.AddParagraph(ShowDescription);
        }


        public void Die()
        {
            Position = null;
        }

        public TProperty GetProperty<TProperty>()
            where TProperty : RoleProperty
        {
            if (!_extraProperties.TryGetValue(typeof(TProperty), out RoleProperty property))
            {
                property = U.Get<TProperty>();
                _extraProperties[typeof(TProperty)] = property;
                property.Role = this;
            }

            return property as TProperty;
        }

        public void RemoveProperty<TProperty>()
            where TProperty : RoleProperty
        {
            _extraProperties.Remove(typeof(TProperty));
        }

        public void Save(IArchiveWriter writer)
        {
            writer.Write(Id);
            Basic.Save(writer);
            Nature.Save(writer);
            IdBuffs.Save(writer);
            Item.Save(writer);
            Equipment.Save(writer);
            CoreSkill.Save(writer);
            FormSkill.Save(writer);
            PassiveSkill.Save(writer);
            Relation.Save(writer);
            
            
            foreach (RoleProperty extraProperty in
                     _extraProperties.Values.ToList()
                         .Where(extraProperty => !extraProperty.CheckNeeded()))
            {
                _extraProperties.Remove(extraProperty.GetType());
            }
            
            writer.Write(_extraProperties.Count);
            foreach (RoleProperty extraProperty in _extraProperties.Values)
            {
                writer.WriteObject(extraProperty);
            }
        }

        public void Load(IArchiveReader reader, WorldCell position)
        {
            Id = reader.ReadInt32();
            Basic.Load(reader);
            Nature.Load(reader);
            IdBuffs.Load(reader);
            Item.Load(reader);
            Equipment.Load(reader, this);
            CoreSkill.Load(reader);
            FormSkill.Load(reader);
            PassiveSkill.Load(reader, this);
            Relation.Load(reader, this, position);
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                RoleProperty property = reader.ReadObject<RoleProperty>();
                _extraProperties[property!.GetType()] = property;
            }
        }

        public string ShowName => Basic.Name;

        public string ShowDescription => Basic.Description;


        public HexUnit Unit { get; set; }

        public void OnDying()
        {
        }

        public void OnEndPlay()
        {
        }
    }
}