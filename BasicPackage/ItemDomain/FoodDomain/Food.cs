#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.PlayerDomain;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
	public sealed class Food : ParasitiferProperty<Food>,
		IItem, IEdible, IEdibleInBattle, IHasFlavor, IHasMouthfeel, IArchivableShown,IArchivable
	{
		public readonly List<IBuffFactory<Role>> RoleBuffFactories = new();
		public readonly List<IBuffFactory<RoleBattleChess>> RoleChessBuffFactories = new();

		public float SpicyLevel { get; set; }

		public float SweetLevel { get; set; }

		public float SourLevel { get; set; }

		public float BitterLevel { get; set; }

		public float SaltyLevel { get; set; }

		public float HardLevel { get; set; }
		public float LimpLevel { get; set; }
		public float WeakLevel { get; set; }
		public float OilyLevel { get; set; }
		public float SlipLevel { get; set; }
		public float SoftLevel { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public IObjectAccessor<Sprite> Icon { get; set; }

		public string ShowName => "菜肴 " + Name;

		public string ShowDescription => Description;

		public IObjectAccessor<Sprite> ShowIcon => Icon;

		public byte Score { get; set; }

		public int SaveIndex { get; set; }

		public void Eating()
		{
			Role role = GameScope.Instance.Role.MainOperationRole;
			foreach (IBuff<Role> buff in RoleBuffFactories.Select(u => u.Get()))
				buff.Install(role);
		}

		public void EatingInBattle()
		{
			RoleBattleChess role =BattleScope.Instance.Context.CurrentRole;
			foreach (IBuff<RoleBattleChess> buff in RoleChessBuffFactories.Select(u => u.Get()))
				buff.Install(role);
		}

		public override void Save(IArchiveWriter writer)
		{
			this.SaveShown(writer);
			this.SaveMouthFeel(writer);
			this.SaveFlavors(writer);
			base.Save(writer);
		}

		public void Load(IArchiveReader reader)
		{
			this.LoadShown(reader);
			this.LoadMouthFeel(reader);
			this.LoadFlavors(reader);
			base.Load(reader, this);
		}

		public void SetContent(Transform transform)
		{
			transform.AddItemHeader(this);
			transform.AddFlavorInfo(this);
			transform.AddMouthFeelInfo(this);
			transform.AddListShown( "效果",Values);
		}
	}
}