#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using System;
using Secyud.Ugf.AssetComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BiographyDomain
{
	public sealed class BodyPartBiography : IBiography
	{
		private readonly int _value;
		private readonly BodyType _bodyType;

		public string ShowName { get; }

		public string ShowDescription { get; }

		public IObjectAccessor<Sprite> ShowIcon { get; }

		public BodyPartBiography(string description, string name, int value, BodyType bodyType,
			Type abType = null, string iconName = null)
		{
			ShowName = name;
			ShowDescription = description;
			if (abType is not null && iconName is not null)
				ShowIcon = SpriteContainer.Create(abType, iconName);
			_value = value;
			_bodyType = bodyType;
		}

		public void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}

		public void OnGameCreation(Role role)
		{
			role.BodyPart[_bodyType].MaxValue += _value;
		}
	}
}