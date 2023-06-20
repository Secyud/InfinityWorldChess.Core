#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.SkillDomain
{
	public class SkillCell : MonoBehaviour
	{
		[SerializeField] private NormalCell Cell;
		[SerializeField] private SButton Install;
		[SerializeField] private SButton Remove;
		[SerializeField] private SkillEquipView View;
		private int _index;

		public NormalCell ViewCell => Cell;

		private void Awake()
		{
			_index = transform.GetSiblingIndex();
			View.OnPrepare(this, _index);
		}

		public void SetInstallable(bool value)
		{
			Install.gameObject.SetActive(value);
			Remove.gameObject.SetActive(value);
		}

		public void OnSelect()
		{
			View.OnSelect(_index);
		}

		public void OnHover()
		{
			View.OnHover(_index);
		}

		public void OnInstall()
		{
			View.OnInstall(_index);
		}

		public void OnRemove()
		{
			View.OnRemove(_index);
		}
	}
}