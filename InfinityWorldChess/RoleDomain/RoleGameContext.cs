#region

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    [Registry(DependScope = typeof(GameScope))]
    public class RoleGameContext : SortedDictionary<int, Role>, IRegistry
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

            WorldMap map = GameScope.Instance.Map.Value;
            Clear();
            int count = reader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                Role role = new();
                WorldCell cell = map.GetCell(reader.ReadInt32()) as WorldCell;
                role.Load(reader, cell);
                this[role.Id] = role;
                if (U.AddStep())
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameSaving()
        {
            using FileStream stream = File.OpenRead(SavePath);
            using DefaultArchiveWriter writer = new(stream);


            List<Role> roles = Values.ToList();

            int count = roles.Count;

            writer.Write(count);

            foreach (Role role in roles)
            {
                writer.Write(role.Relation.Position.Index);
                role.Save(writer);
                if (U.AddStep())
                    yield return null;
            }
        }

        public virtual IEnumerator OnGameCreation()
        {
            string path = GameScope.Instance.World.WorldSetting
                .GetDataDirectory("roles.binary");

            List<RoleTemplate> roles = U.Tm.ConstructListFromFile<RoleTemplate>(path);

            foreach (RoleTemplate template in roles)
            {
                template.GenerateRole();
            }

            if (U.AddStep())
                yield return null;
        }
    }
}