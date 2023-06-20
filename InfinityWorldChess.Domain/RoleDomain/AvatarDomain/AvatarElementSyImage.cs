#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class AvatarElementSyImage : AvatarElementImage
	{
		[SerializeField] private float RangeY = 0.1f;
		[SerializeField] private float SizeRangeMax = 1.5f;
		[SerializeField] private float SizeRangeMin = 0.5f;


		public void SetElement(Sprite image, Vector2 anchor, float scale, Vector2 vector)
		{
			base.SetElement(
				image,
				scale * Mathf.Lerp(SizeRangeMin, SizeRangeMax, vector.x),
				anchor + new Vector2(0, RangeY * vector.y)
			);
		}
	}
}