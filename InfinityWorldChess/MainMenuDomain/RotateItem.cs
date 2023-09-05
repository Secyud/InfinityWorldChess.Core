using UnityEngine;

namespace InfinityWorldChess.MainMenuDomain
{
	public class RotateItem:MonoBehaviour
	{
		[SerializeField]
		private float Speed;
		private float _d;
		private void Update()
		{
			transform.rotation = Quaternion.Euler(0,0,_d);
			_d += Time.deltaTime * Speed;
		}
	}
}