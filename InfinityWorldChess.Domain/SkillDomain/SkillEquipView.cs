#region

using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public abstract class SkillEquipView : MonoBehaviour
	{
		[SerializeField] protected SkillCell[] Cells;

		protected Role TargetRole { get; set; }

		public abstract void OnSelect(int index);

		public abstract void OnHover(int index);

		public abstract void OnInstall(int index);

		public abstract void OnRemove(int index);

		public virtual void OnInitialize(Role role)
		{
			TargetRole = role;
			bool b = role == Og.Get<GameScope,PlayerGameContext>().Role ||
			         GameScope.PlayerGameContext.PlayerSetting.YunChouWeiWo;
			foreach (SkillCell skillCell in Cells)
				skillCell.SetInstallable(b);
		}

		public virtual void OnPrepare(SkillCell cell, int index)
		{
			Cells[index] = cell;
		}
	}
}