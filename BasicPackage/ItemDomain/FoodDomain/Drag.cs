using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public class Drag: IdBuffProperty<Drag>,
		IItem, IEdible, IEdibleInBattle, IHasFlavor, IArchivableShown,IArchivable
	{
		public readonly List<IBuff<Role>> RoleBuffs = new();
		public readonly List<IBuff<BattleRole>> BattleRoleBuffs = new();

		public string Name { get; set; }

		public string Description { get; set; }

		public IObjectAccessor<Sprite> Icon { get; set; }

		public string ShowName => "菜肴 " + Name;

		public string ShowDescription => Description;

		public IObjectAccessor<Sprite> ShowIcon => Icon;
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
			this.SaveShown(writer);
			this.SaveFlavors(writer);
			base.Save(writer);
		}

		public override void Load(IArchiveReader reader)
		{
			this.LoadShown(reader);
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