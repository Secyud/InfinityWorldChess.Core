#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.IO;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public class TestBuff : IBuff<Role>, IArchivable
	{
		public void SetContent(Transform transform)
		{
		}

		public string Name { get; }

		public string Description { get; }

		public IObjectAccessor<Sprite> Icon { get; }

		public void Install(Role target)
		{
		}

		public void UnInstall(Role target)
		{
		}

		public void Overlay(IBuff<Role> finishBuff)
		{
		}

		public bool Visible { get; }


		public void Save(BinaryWriter writer)
		{
		}

		public void Load(BinaryReader reader)
		{
		}
	}
}