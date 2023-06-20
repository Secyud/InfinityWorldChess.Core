﻿#region

using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.InputManaging;
using UnityEngine;

#endregion

namespace InfinityWorldChess.InputDomain
{
	[RequireComponent(typeof(SButton))]
	public class InputComponent : MonoBehaviour
	{
		[SerializeField] private InputKeyWord KeyWord;
		[SerializeField] private RectTransform UiLayer;
		private InputService _service;
		private IInputKeyMapService _map;
		private SButton _button;

		private void Awake()
		{
			_service = Og.DefaultProvider.Get<InputService>();
			_map = Og.DefaultProvider.Get<IInputKeyMapService>();
			_button = GetComponent<SButton>();
		}

		private void OnEnable()
		{
			_service.AddEvent(_map[KeyWord], UiLayer, _button.onClick);
		}


		private void OnDisable()
		{
			_service.RemoveEvent(_map[KeyWord], UiLayer);
		}
	}
}