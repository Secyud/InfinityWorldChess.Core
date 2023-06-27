#region

using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.ButtonComponents;
using Secyud.Ugf.Layout;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class ButtonGroup : LayoutGroupTrigger
	{
		private static ButtonGroup _instance;
		[SerializeField] private SButton ButtonTemplate;
		[SerializeField] private SText TextTemplate;

		public void OnInitialize<TItem>(TItem target,
			IEnumerable<ButtonRegistration<TItem>> buttons,
			UnityAction refreshAction = null)
		{
			if (!Float)
				for (int i = 0; i < transform.childCount; i++)
					Destroy(transform.GetChild(i).gameObject);

			foreach (ButtonRegistration<TItem> button in buttons.SelectVisibleFor(target))
			{
				button.Target = target;
				SButton b = ButtonTemplate.Instantiate(transform);
				b.Bind(button.Trigger);
				if (refreshAction is not null)
					b.onClick.AddListener(refreshAction);
				if (Float)
					b.onClick.AddListener(() => Destroy(gameObject));
				SText t = TextTemplate.Instantiate(b.transform);
				t.text = U.T[button.ShowName];
				button.SetButton(b);
			}
			if (Float && LayoutElement is GridLayoutGroup group)
			{
				group.constraintCount = (int)Math.Ceiling(RectTransform.rect.height / Screen.height);
				RectTransform.SetRectPosition(
					UgfUnityExtensions.GetMousePosition() - new Vector2(8, 8),
					Vector2.zero
				);
			}
			enabled = true;
		}

		public ButtonGroup Create<TItem>(TItem target,
			IEnumerable<ButtonRegistration<TItem>> buttons,
			UnityAction refreshAction = null)
		{
			ButtonGroup group = Instantiate(this, U.Canvas.transform);
			group.OnInitialize(target, buttons, refreshAction);
			group.Replace(ref _instance);
			return group;
		}
	}
}