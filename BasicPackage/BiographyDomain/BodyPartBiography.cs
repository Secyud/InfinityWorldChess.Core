#region

using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
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

		public string Name { get; }

		public string Description { get; }

		public IObjectAccessor<Sprite> Icon { get; }

		public BodyPartBiography(string description, string name, int value, BodyType bodyType,
			Type abType = null, string iconName = null)
		{
			Name = name;
			Description = description;
			if (abType is not null && iconName is not null)
				Icon = SpriteContainer.Create(abType, iconName);
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