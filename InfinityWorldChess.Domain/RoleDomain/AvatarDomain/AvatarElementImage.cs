#region

using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class AvatarElementImage : SImage
	{
		protected Vector2 Anchor;

		protected override void Awake()
		{
			base.Awake();
			Anchor = rectTransform.anchorMin;
		}

		public void SetElement(Sprite image, float scale, Vector2 anchor)
		{
			Sprite = image;
			SetNativeSize();
			Vector2 result = Anchor + anchor;
			rectTransform.anchorMin = result;
			rectTransform.anchorMax = result;
			rectTransform.sizeDelta *= scale;
		}
	}
}