#region

using InfinityWorldChess.GlobalDomain;
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
			Role role = GlobalScope.Instance.RoleContext.Get(unit.Id);
			return role?.GetSpeed() ?? 1;
		}
	}
}