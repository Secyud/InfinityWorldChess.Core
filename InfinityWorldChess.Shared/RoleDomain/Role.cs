#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using System;
using System.Collections.Generic;
using System.IO;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public partial class Role : IHasContent,IOnBattleRoleInitialize
	{
		public int Id { get; set; }

		private readonly Dictionary<Type, RoleProperty> _extraProperties = new();

		public int ExtraPropertyCount => _extraProperties.Count;

		public void SetContent(Transform transform)
		{
			U.Get<IRoleService>().SetContent(transform, this);
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
			Buffs.Save(writer);
			Item.Save(writer);
			Equipment.Save(writer);
			CoreSkill.Save(writer);
			FormSkill.Save(writer);
			PassiveSkill.Save(writer);
			Relation.Save(writer);
			writer.Write(_extraProperties.Count);
			foreach (RoleProperty extraProperty in _extraProperties.Values)
				writer.Write(extraProperty);
		}

		public void Load(IArchiveReader reader, WorldChecker position)
		{
			Id = reader.ReadInt32();
			Basic.Load(reader);
			Nature.Load(reader);
			Buffs.Load(reader, this);
			Item.Load(reader);
			Equipment.Load(reader, this);
			CoreSkill.Load(reader);
			FormSkill.Load(reader);
			PassiveSkill.Load(reader, this);
			Relation.Load(reader, this, position);
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				RoleProperty property = reader.ReadArchiving<RoleProperty>();
				_extraProperties[property!.GetType()] = property;
			}
		}

		public string ShowName => Basic.Name;

		public string ShowDescription => Basic.Description;

	}
}