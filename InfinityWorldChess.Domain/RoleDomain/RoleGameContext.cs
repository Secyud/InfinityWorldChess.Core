#region

using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class RoleGameContext : IScoped, IOnGameArchiving
	{
		private int _maxRoleId;

		public int MaxRoleId => _maxRoleId++;

		public Dictionary<int, Role> Roles { get; private set; }

		public Role MainOperationRole { get; set; }

		public bool IsPlayer()
		{
			return MainOperationRole.Id == 0;
		}

		public bool IsPlayerView()
		{
			return IsPlayer() || GameScope.PlayerGameContext.PlayerSetting.YunChouWeiWo;
		}

		public Role SupportOperationRole { get; set; }

		public Role Get(int id)
		{
			Roles.TryGetValue(id, out Role role);
			return role;
		}

		public virtual void OnGameLoading(LoadingContext context)
		{
			BinaryReader reader = context.GetReader(nameof(RoleGameContext));
			WorldGameContext gmCtx = GameScope.WorldGameContext;

			Roles = new Dictionary<int, Role>();
			int count = reader.ReadInt32();

			for (int i = 0; i < count; i++)
			{
				Role role = new();
				WorldChecker checker = gmCtx.Checkers[reader.ReadInt32()];
				role.Load(reader, checker);
				Roles[role.Id] = role;
			}
			_maxRoleId = Roles.Keys.Max();
		}

		public virtual void OnGameSaving(SavingContext context)
		{
			BinaryWriter writer = context.GetWriter(nameof(RoleGameContext));


			int count = Roles.Count;
			writer.Write(count);

			for (int i = 0; i < count; i++)
			{
				writer.Write(Roles[i].Relation.Position.Cell.Index);
				Roles[i].Save(writer);
			}
		}

		public virtual void OnGameCreation()
		{
			Roles = new Dictionary<int, Role>();
			_maxRoleId = 1;
			foreach (Role role in Og.DefaultProvider.Get<IRoleGenerator>().GenerateRole())
			{
				int id = MaxRoleId;
				Roles[id] = role;
				role.Id = id;
			}
		}
	}
}