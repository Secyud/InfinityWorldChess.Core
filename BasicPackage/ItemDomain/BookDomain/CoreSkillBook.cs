#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using System;
using System.IO;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.BookDomain
{
	public class CoreSkillBook : SkillBookBase
	{
		protected override string SkillName => Skill.ShowName;

		public ICoreSkill Skill { get; set; }

		protected override void SetMiddleContent(Transform transform)
		{
			if (Skill is IHasContent c)
				c.SetContent(transform);
		}

		public override void Save(BinaryWriter writer)
		{
			base.Save(writer);
			writer.WriteNullableArchiving(Skill);
		}

		public override void Load(BinaryReader reader)
		{
			base.Load(reader);
			Skill = reader.ReadNullableArchiving<ICoreSkill>();
		}

		protected override void Reading(Role role)
		{
			role.CoreSkill.LearnedSkills.Add(Skill);
		}
	}
}