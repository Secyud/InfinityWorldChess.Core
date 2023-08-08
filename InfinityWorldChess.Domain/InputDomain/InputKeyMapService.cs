#region

using System.Collections.Generic;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.InputDomain
{
	public class InputKeyMapService :IRegistry
	{
		private readonly Dictionary<InputKeyWord, KeyCode> _map = new();

		public InputKeyMapService()
		{
			_map[InputKeyWord.Cancel] = KeyCode.Escape;
			_map[InputKeyWord.Submit] = KeyCode.Return;
		}

		public KeyCode this[InputKeyWord word]
		{
			get => _map[word];
			set => _map[word] = value;
		}
	}
}