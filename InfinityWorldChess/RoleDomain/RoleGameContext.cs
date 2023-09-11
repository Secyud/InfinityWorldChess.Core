#region

using System.Collections;
using System.IO;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class RoleGameContext: IRegistry
	{
		public Role MainOperationRole { get; set; }

		public Role SupportOperationRole { get; set; }

		private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(RoleGameContext));

		public bool IsPlayer()
		{
			return MainOperationRole.Id == 0;
		}

		public bool IsPlayerView()
		{
			return IsPlayer() || GameScope.Instance.Player.PlayerSetting.YunChouWeiWo;
		}

		public virtual IEnumerator OnGameLoading()
		{
			using FileStream stream = File.OpenRead(SavePath);
			using DefaultArchiveReader reader = new(stream);
			RoleContext roleContext = GlobalScope.Instance.RoleContext;
			yield return roleContext.Load(reader);
		}

		public virtual IEnumerator OnGameSaving()
		{
			using FileStream stream = File.OpenRead(SavePath);
			using DefaultArchiveWriter writer = new(stream);

			RoleContext role = GlobalScope.Instance.RoleContext;
			yield return role.Save(writer);
		}

		public virtual IEnumerator OnGameCreation()
		{
			U.Get<RoleGenerator>().GenerateRole();
			if (U.AddStep())
				yield return null;
		}
	}
}