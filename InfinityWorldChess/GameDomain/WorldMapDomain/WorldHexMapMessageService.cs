#region

using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.UgfHexMap;

#endregion

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
	[Registry(DependScope = typeof(GameScope))]
	public class WorldHexMapMessageService :  UgfHexMapMessageService
	{
		public override float GetSpeed(HexUnit unit)
		{
			GameScope.Instance.Role.TryGetValue(unit.Id, out Role role);
			return role?.GetSpeed() ?? 1;
		}

		public override HexGrid Grid => GameScope.Instance.Map;
	}
}