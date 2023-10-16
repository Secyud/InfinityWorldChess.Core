using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldMapDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;

namespace InfinityWorldChess.RoleDomain
{
    [Registry(DependScope = typeof(GlobalScope))]
    public class RoleContext:IRegistry
    {
        private readonly SortedDictionary<int, Role> _roles = new();
        private int _max;
        public bool CheckMax { get; set; } = true;
        
        public int GetNewId()
        {
            int id = _max++;
            if (CheckMax && _roles.ContainsKey(id))
            {
                _max = _roles.Keys.Max() + 1;
                CheckMax = false;
            }

            return id;
        }

        public void AddRole(Role role)
        {
            _roles[role.Id] = role;
        }

        public void RemoveRole(Role role)
        {
            _roles.Remove(role.Id);
        }

        public IEnumerator Save(IArchiveWriter writer)
        {
            List<Role> roles = _roles.Values.ToList();
            int count = roles.Count;
            writer.Write(count);

            foreach (Role role in roles)
            {
                writer.Write(role.Relation.Position.Cell.Index);
                role.Save(writer);
                if (U.AddStep())
                    yield return null;
            }
        }

        public IEnumerator Load(IArchiveReader reader)
        {
            WorldMap map = GameScope.Instance.Map.Value;
            _roles.Clear();
            int count = reader.ReadInt32();
			
            for (int i = 0; i < count; i++)
            {
                Role role = new(true);
                HexCell cell = map.GetCell(reader.ReadInt32());
                role.Load(reader, cell.Get<WorldCell>());
                AddRole(role);
                if (U.AddStep())
                    yield return null;
            }
        }

        public Role Get(int unitId)
        {
            return _roles[unitId];
        }
    }
}