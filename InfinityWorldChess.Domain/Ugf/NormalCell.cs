#region

using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;

#endregion

namespace InfinityWorldChess.Ugf
{
	public class NormalCell : ImageCell
	{
		public SText Name;

		public override void OnInitialize(ICanBeShown item)
		{
			Name.text = U.T[item?.ShowName];
			base.OnInitialize(item);
		}
	}
}