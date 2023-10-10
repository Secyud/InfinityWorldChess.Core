#region

using Secyud.Ugf;
using System;
using System.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role : IUnitBase,IHasContent
	{
		public int Id { get; private set; }

		private readonly Dictionary<Type, RoleProperty> _extraProperties = new();
		private HexUnit _unit;

		public Role(bool load = false)
		{
			if (!load)
			{
				Id = GlobalScope.Instance.RoleContext.GetNewId();
			}
		}
		
		public int ExtraPropertyCount => _extraProperties.Count;

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
			if (_extraProperties.TryGetValue(typeof(TProperty), out RoleProperty property))
				return property as TProperty;

			return null;
		}

		public void SetProperty<TProperty>(TProperty property = null)
			where TProperty : RoleProperty
		{
			if (property is null)
				_extraProperties.Remove(typeof(TProperty));
			else
				_extraProperties[typeof(TProperty)] = property;
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
			writer.Write(_extraProperties.Count);
			foreach (RoleProperty extraProperty in _extraProperties.Values)
				writer.WriteObject(extraProperty);
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