#region

using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class AvatarElementXySrImage : AvatarElementImage
	{
		[SerializeField] private float SizeRangeMax = 1.5f;
		[SerializeField] private float SizeRangeMin = 0.5f;
		[SerializeField] private float RotationMax = 30f;
		[SerializeField] private float RotationMin = -30f;
		[SerializeField] private float RangeX = 0.1f;
		[SerializeField] private float RangeY = 0.1f;

		public void SetElement(Sprite image, Vector2 anchor, float scale, Vector4 vector)
		{
			base.SetElement(
				image,
				scale * Mathf.Lerp(SizeRangeMin, SizeRangeMax, vector.z),
				anchor + new Vector2(RangeX * vector.x, RangeY * vector.y)
			);

			rectTransform.rotation = Quaternion.Euler(
				0, 0, Mathf.Lerp(RotationMin, RotationMax, vector.w)
			);
		}
	}
}