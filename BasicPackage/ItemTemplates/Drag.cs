using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
	public class Drag: CustomizableItem<Drag>,
		IItem, IEdible, IEdibleInBattle, IHasFlavor,IArchivable
	{
		public readonly List<IBuff<Role>> RoleBuffs = new();
		public readonly List<IBuff<BattleRole>> BattleRoleBuffs = new();

		public int Score { get; set; }

		public int SaveIndex { get; set; }

		public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];

		protected override Drag Target => this;

		public void Eating(Role role)
		{
			foreach (IBuff<Role> buff in RoleBuffs)
				buff.Install(role);
		}

		public void EatingInBattle(BattleRole role)
		{
			foreach (IBuff<BattleRole> buff in BattleRoleBuffs)
				buff.Install(role);
		}

		public override void Save(IArchiveWriter writer)
		{
			this.SaveFlavors(writer);
			base.Save(writer);
		}

		public override void Load(IArchiveReader reader)
		{
			this.LoadFlavors(reader);
			base.Load(reader);
		}

		public void SetContent(Transform transform)
		{
			transform.AddItemHeader(this);
			transform.AddFlavorInfo(this);
			transform.AddListShown( "效果",Values);
		}
	}
}