using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.BattleBuffs.Abstractions
{
	public abstract class BattleShownBuffBase : ResourcedBase, IBuffCanBeShown<RoleBattleChess>
	{
		public RoleBattleChess Launcher { get; set; }

		protected override void SetDefaultValue()
		{
			ShowIcon = AtlasSpriteContainer.Create(
				IwcAb.Instance, Descriptor,0);
		}

		public virtual void Install(RoleBattleChess target)
		{
		}

		public virtual void UnInstall(RoleBattleChess target)
		{
		}

		public virtual void Overlay(IBuff<RoleBattleChess> finishBuff)
		{
		}

		public virtual bool Visible => true;

		public abstract string ShowName { get; }

		public abstract string ShowDescription { get; }

		public IObjectAccessor<Sprite> ShowIcon { get; set; }

		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}
	}
}