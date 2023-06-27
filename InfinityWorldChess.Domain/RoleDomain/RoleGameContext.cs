#region

using System.Collections;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.WorldDomain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	[Registry(LifeTime = DependencyLifeTime.Scoped,DependScope = typeof(GameScope))]
	public class RoleGameContext
	{
		private int _maxRoleId;

		public int MaxRoleId => _maxRoleId++;

		public Dictionary<int, Role> Roles { get; private set; }

		public Role MainOperationRole { get; set; }

		private static readonly string SavePath = SharedConsts.SaveFilePath(nameof(RoleGameContext));

		public bool IsPlayer()
		{
			return MainOperationRole.Id == 0;
		}

		public bool IsPlayerView()
		{
			return IsPlayer() || GameScope.Instance.Player.PlayerSetting.YunChouWeiWo;
		}

		public Role SupportOperationRole { get; set; }

		public Role Get(int id)
		{
			Roles.TryGetValue(id, out Role role);
			return role;
		}

		public virtual IEnumerator OnGameLoading()
		{
			using FileStream stream = File.OpenRead(SavePath);
			using DefaultArchiveReader reader = new(stream);
			WorldGameContext gmCtx = GameScope.Instance.World;

			Roles = new Dictionary<int, Role>();
			int count = reader.ReadInt32();
			
			for (int i = 0; i < count; i++)
			{
				Role role = new();
				WorldChecker checker = gmCtx.Checkers[reader.ReadInt32()];
				role.Load(reader, checker);
				Roles[role.Id] = role;
				if (U.AddStep())
					yield return null;
			}
			_maxRoleId = Roles.Keys.Max();
		}

		public virtual IEnumerator OnGameSaving()
		{
			using FileStream stream = File.OpenRead(SavePath);
			using DefaultArchiveWriter writer = new(stream);

			int count = Roles.Count;
			writer.Write(count);

			for (int i = 0; i < count; i++)
			{
				writer.Write(Roles[i].Relation.Position.Cell.Index);
				Roles[i].Save(writer);
				if (U.AddStep())
					yield return null;
			}
		}

		public virtual IEnumerator OnGameCreation()
		{
			Roles = new Dictionary<int, Role>();
			_maxRoleId = 1;
			foreach (Role role in U.Get<IRoleGenerator>().GenerateRole())
			{
				int id = MaxRoleId;
				Roles[id] = role;
				role.Id = id;
				if (U.AddStep())
					yield return null;
			}
		}
	}
}