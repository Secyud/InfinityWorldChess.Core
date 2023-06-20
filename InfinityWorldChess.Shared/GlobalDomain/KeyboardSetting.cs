#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GlobalDomain
{
	public class KeyboardSetting
	{
		public readonly List<Tuple<KeyCode, Action>> Pairs = new();
	}
}