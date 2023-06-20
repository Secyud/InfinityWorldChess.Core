using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.Modularity;
using System.IO;
using UnityEngine;

namespace InfinityWorldChess.ArchivingComponent
{
	public class IwcSlot : ISlot
	{
		public IwcSlot(int id)
		{
			Id = id;
		}

		public int Id { get; }

		public string Name => _basicProperty?.Name ?? "空存档";

		private Role.BasicProperty _basicProperty;

		public void PrepareSlotSaving(SavingContext context)
		{
			using BinaryWriter writer = context.GetWriter("slot.slot");
			GameScope.PlayerGameContext.Role.Basic.Save(writer);
		}

		public void PrepareSlotLoading()
		{
			string path = Path.Combine(Og.ArchivingPath, Id.ToString(), "slot.slot");
			if (File.Exists(path))
			{
				using FileStream fileStream = File.Open(path, FileMode.Open);
				BinaryReader reader = new(fileStream);
				_basicProperty = new Role.BasicProperty();
				_basicProperty.Load(reader);
			}
			else
			{
				_basicProperty = null;
			}
		}

		public void SetContent(RectTransform content)
		{
			if (_basicProperty is null)
				content.gameObject.SetActive(false);
			else
			{
				content.gameObject.SetActive(true);
				RoleAvatarViewer viewer = content.GetComponent<RoleAvatarViewer>();
				viewer.OnInitialize(_basicProperty);
			}
		}
	}
}