#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System;
using System.IO;
using InfinityWorldChess.PlayerDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
	public abstract class SkillBookBase : IItem, IReadable, IArchivable
	{
		public string ShowName => "书".PointAfter() + SkillName;

		public string ShowDescription => $"阅读可以习得{SkillName}";

		protected abstract string SkillName { get; }

		public IObjectAccessor<Sprite> ShowIcon => Icon;

		public IObjectAccessor<Sprite> Icon { get; set; }

		public int SaveIndex { get; set; }

		public virtual byte Score => 3;

		public void SetContent(Transform transform)
		{
			transform.AddItemHeader(this);
			SetMiddleContent(transform);
		}

		protected virtual void SetMiddleContent(Transform transform)
		{
		}

		public void Reading()
		{
			RoleGameContext roleContext = GameScope.RoleGameContext;
			Role role = roleContext.MainOperationRole;
			Reading(role);
			role.Item.Remove(this);
		}

		protected abstract void Reading(Role role);

		public virtual void Save(BinaryWriter writer)
		{
			writer.WriteNullableArchiving(Icon);
		}

		public virtual void Load(BinaryReader reader)
		{
			Icon = reader.ReadNullableArchiving<IObjectAccessor<Sprite>>();
		}
	}
}