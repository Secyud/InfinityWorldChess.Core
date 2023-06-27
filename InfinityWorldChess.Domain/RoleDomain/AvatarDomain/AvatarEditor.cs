#region

using Secyud.Ugf.BasicComponents;
using System;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using Secyud.Ugf;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class AvatarEditor : MonoBehaviour
	{
		[SerializeField] private RoleAvatarViewer Viewer;
		[SerializeField] protected SSlider[] Sliders;

		private RoleAvatar _avatar;
		private AvatarResourceGroup _group;
		private RoleResourceManager _manager;

		protected void Awake()
		{
			_manager = U.Get<RoleResourceManager>();
		}

		public void OnInitialize(Role.BasicProperty basic)
		{
			_avatar = basic.Avatar;
			_group = basic.Female ? _manager.Female : _manager.Male;
			SetSlider(_group.BackItem.KeyList, 0);
			SetSlider(_group.BackHair.KeyList, 1);
			SetSlider(_group.Body.KeyList, 2);
			SetSlider(_group.Head.KeyList, 3);
			SetSlider(_group.HeadFeature.KeyList, 4);
			SetSlider(_group.Nose.KeyList, 5);
			SetSlider(_group.Mouth.KeyList, 6);
			SetSlider(_group.Eye.KeyList, 7);
			SetSlider(_group.Brow.KeyList, 8);
			SetSlider(_group.FrontHair.KeyList, 9);

			Viewer.OnInitialize(basic);
		}

		private void SetSlider(IReadOnlyList<int> keyList, int index)
		{
			Sliders[index].minValue = 0;
			Sliders[index].maxValue = Math.Max(keyList.Count - 1, 0);
			Sliders[index].value = 0;
		}

		public void SetBackItem(float value)
		{
			_avatar.BackItem.Id = _group.BackItem.KeyList.Pick(value);
			Viewer.SetBackItem(_avatar.BackItem);
		}

		public void SetBackHair(float value)
		{
			_avatar.BackHair.Id = _group.BackHair.KeyList.Pick(value);
			Viewer.SetBackHair(_avatar.BackHair);
		}

		public void SetBody(float value)
		{
			_avatar.Body.Id = _group.Body.KeyList.Pick(value);
			Viewer.SetBody(_avatar.Body);
		}

		public void SetHead(float value)
		{
			_avatar.Head.Id = _group.Head.KeyList.Pick(value);
			Viewer.SetHead(_avatar.Head);
		}

		public void SetHeadFeature(float value)
		{
			_avatar.HeadFeature.Id = _group.HeadFeature.KeyList.Pick(value);
			Viewer.SetHeadFeature(_avatar.HeadFeature);
		}

		public void SetHeadFeaturePosition(Vector2 value)
		{
			_avatar.HeadFeature.X = (byte)value.x;
			_avatar.HeadFeature.Y = (byte)value.y;
			Viewer.SetHeadFeature(_avatar.HeadFeature);
		}

		public void SetHeadFeatureRotation(float value)
		{
			_avatar.HeadFeature.W = (byte)value;
			Viewer.SetHeadFeature(_avatar.HeadFeature);
		}

		public void SetHeadFeatureScale(float value)
		{
			_avatar.HeadFeature.Z = (byte)value;
			Viewer.SetHeadFeature(_avatar.HeadFeature);
		}

		public void SetNose(float value)
		{
			_avatar.NoseMouth.Id1 = _group.Nose.KeyList.Pick(value);
			Viewer.SetNose(_avatar.NoseMouth);
		}

		public void SetNoseHeight(float value)
		{
			_avatar.NoseMouth.Y = (byte)value;
			Viewer.SetNose(_avatar.NoseMouth);
		}

		public void SetNoseScale(float value)
		{
			_avatar.NoseMouth.X = (byte)value;
			Viewer.SetNose(_avatar.NoseMouth);
		}

		public void SetMouth(float value)
		{
			_avatar.NoseMouth.Id2 = _group.Mouth.KeyList.Pick(value);
			Viewer.SetMouth(_avatar.NoseMouth);
		}

		public void SetMouthHeight(float value)
		{
			_avatar.NoseMouth.W = (byte)value;
			Viewer.SetMouth(_avatar.NoseMouth);
		}

		public void SetMouthScale(float value)
		{
			_avatar.NoseMouth.Z = (byte)value;
			Viewer.SetMouth(_avatar.NoseMouth);
		}

		public void SetEye(float value)
		{
			_avatar.Eye.Id = _group.Eye.KeyList.Pick(value);
			Viewer.SetEye(_avatar.Eye);
		}

		public void SetEyePosition(Vector2 value)
		{
			_avatar.Eye.X = (byte)value.x;
			_avatar.Eye.Y = (byte)value.y;
			Viewer.SetEye(_avatar.Eye);
		}

		public void SetEyeRotation(float value)
		{
			_avatar.Eye.W = (byte)value;
			Viewer.SetEye(_avatar.Eye);
		}

		public void SetEyeScale(float value)
		{
			_avatar.Eye.Z = (byte)value;
			Viewer.SetEye(_avatar.Eye);
		}

		public void SetBrow(float value)
		{
			_avatar.Brow.Id = _group.Brow.KeyList.Pick(value);
			Viewer.SetBrow(_avatar.Brow);
		}

		public void SetBrowPosition(Vector2 value)
		{
			_avatar.Brow.X = (byte)value.x;
			_avatar.Brow.Y = (byte)value.y;
			Viewer.SetBrow(_avatar.Brow);
		}

		public void SetBrowRotation(float value)
		{
			_avatar.Brow.W = (byte)value;
			Viewer.SetBrow(_avatar.Brow);
		}

		public void SetBrowScale(float value)
		{
			_avatar.Brow.Z = (byte)value;
			Viewer.SetBrow(_avatar.Brow);
		}

		public void SetFrontHair(float value)
		{
			_avatar.FrontHair.Id = _group.FrontHair.KeyList.Pick(value);
			Viewer.SetFrontHair(_avatar.FrontHair);
		}
	}
}