#region

using Secyud.Ugf.BasicComponents;
using System;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class BodyPartSelectComponent : MonoBehaviour
	{
		[SerializeField] private SButton[] Buttons;

		public event Action<int> EnsureAction;

		private SText[] _texts;
		private int _selectPart = -1;

		private void Awake()
		{
			_texts = new SText[MainPackageConsts.MaxBodyPartsCount];
			for (int i = 0; i < MainPackageConsts.MaxBodyPartsCount; i++)
				_texts[i] = Buttons[i].GetComponent<SText>();
		}

		public void OnEnsure()
		{
			EnsureAction?.Invoke(_selectPart);
			Die();
		}

		public void Die()
		{
			Destroy(gameObject);
		}

		public void SelectPart(int index)
		{
			if (CheckIndex())
				_texts[_selectPart].color = Color.black;
			_selectPart = index;
			if (CheckIndex())
				_texts[_selectPart].color = Color.green;
		}

		private bool CheckIndex()
		{
			return _selectPart is >= 0 and < MainPackageConsts.MaxBodyPartsCount;
		}

		public void OnInitialize(byte code)
		{
			for (int i = 0; i < MainPackageConsts.MaxBodyPartsCount; i++)
				Buttons[i].enabled = (1u << i & code) > 0;
		}

		public BodyPartSelectComponent Create(byte code)
		{
			BodyPartSelectComponent ret = this.Instantiate(U.Canvas.transform);

			ret.OnInitialize(code);

			return ret;
		}
	}
}