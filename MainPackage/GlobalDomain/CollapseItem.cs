#region

using Secyud.Ugf.LayoutComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public class CollapseItem : MonoBehaviour
	{
		private LayoutGroupTrigger _layoutTrigger;
		private float _record;

		private void Awake()
		{
			transform.parent.TryGetComponent(out _layoutTrigger);
		}

		public void Collapse(bool b)
		{
			if (_layoutTrigger is null)
				return;

			gameObject.SetActive(!b);
			_layoutTrigger.enabled = true;
			enabled = true;
		}
	}
}