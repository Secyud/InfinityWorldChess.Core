using Secyud.Ugf.BasicComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.Ugf
{
	public class SelectOptionCell : MonoBehaviour
	{
		[SerializeField] private SText Content;
		[SerializeField] private SText Id;
		[SerializeField] private int Size;

		private RectTransform _rectTransform;
		private UnityAction _action;
		private int _id = -1;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public void OnClick()
		{
			_action?.Invoke();
			_action = null;
		}

		private void Update()
		{
			if (_id < 0)
				enabled = false;

			if (Input.GetKeyUp(KeyCode.Alpha0 + _id))
				OnClick();
		}

		public void OnInitialize(int id, UnityAction action, string text)
		{
			Content.text = text;
			_id = id;
			_action = action;
			Id.text = $"({id})";
			// ReSharper disable once PossibleLossOfFraction
			_rectTransform.sizeDelta = new Vector2(0, (text.Length / Size+1) * 24);
			enabled = true;
		}
	}
}