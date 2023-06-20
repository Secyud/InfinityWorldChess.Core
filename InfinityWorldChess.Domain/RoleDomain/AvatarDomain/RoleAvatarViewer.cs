#region

using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public sealed class RoleAvatarViewer : RoleAvatarCell
	{
		[SerializeField] private AvatarElementImage BackItem;
		[SerializeField] private AvatarElementImage Body;

		protected override float MultipleSize => 1;

		protected override void Awake()
		{
			Resource = Og.DefaultProvider.Get<RoleResourceManager>();
			Size = Content.rect.width;
			Content.pivot = Content.anchorMin = Content.anchorMax = new Vector2(0.5f, 1);
			Content.anchoredPosition = Vector2.zero;
			Content.sizeDelta = new Vector2(Size, Size * 1.5f);
			Size /= 512;
		}


		public void SetBackItem(RoleAvatar.AvatarElement element)
		{
			SetElement(BackItem, Group.BackItem.Get(element.Id));
		}


		public void SetBody(RoleAvatar.AvatarElement element)
		{
			SetElement(Body, Group.Body.Get(element.Id));
		}

		protected override void SetAvatar(Role.BasicProperty basic)
		{
			base.SetAvatar(basic);
			RoleAvatar avatar = basic.Avatar;
			SetBackItem(avatar.BackItem);
			SetBody(avatar.Body);
		}

		public override void Clear()
		{
			base.Clear();
			BackItem.Sprite = null;
			Body.Sprite = null;
		}
	}
}