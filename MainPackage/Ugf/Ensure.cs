using System;
using UnityEngine;

namespace InfinityWorldChess.Ugf
{
	public sealed class Ensure:MonoBehaviour
	{
		public event Action EnsureAction;

		public void OnEnsureAction()
		{
			EnsureAction?.Invoke();
		}
	}
}