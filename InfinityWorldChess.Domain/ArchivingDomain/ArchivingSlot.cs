using System.IO;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.Archiving;
using UnityEngine;

namespace InfinityWorldChess.ArchivingDomain
{
	public class ArchivingSlot 
	{
		public ArchivingSlot(int id)
		{
			Id = id;
		}

		public int Id { get; }

		public string Name => _basicProperty?.Name ?? "空存档";

		private Role.BasicProperty _basicProperty;


		private static string SavePath => SharedConsts.SaveFilePath("slot");
		
		public void PrepareSlotSaving()
		{
			SharedConsts.SaveFolder = Id;
			using FileStream stream = File.OpenWrite(SavePath);
			using DefaultArchiveWriter writer = new (stream);
			GameScope.Instance.Player.Role.Basic.Save(writer);
		}

		public void PrepareSlotLoading()
		{
			string path = SavePath;
			if (File.Exists(path))
			{
				using FileStream fileStream = File.OpenRead(path);
				DefaultArchiveReader reader = new(fileStream);
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
		
		public bool Exist => File.Exists(SavePath);
	}
}