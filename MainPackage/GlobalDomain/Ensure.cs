using System;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
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