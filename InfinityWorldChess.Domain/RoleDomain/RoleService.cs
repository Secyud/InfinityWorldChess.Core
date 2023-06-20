#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.DependencyInjection;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleService : IRoleService, ITransient
	{
		private readonly RoleResourceManager _resourceManager;

		public RoleService(RoleResourceManager resourceManager)
		{
			_resourceManager = resourceManager;
		}

		public void SetSmallContent(Transform cell, Role role)
		{
			
			
		}

		public void SetContent(Transform transform, Role role)
		{
			transform.AddTitle1(role.ShowName);
			transform.AddParagraph(role.ShowDescription);
		}
	}
}