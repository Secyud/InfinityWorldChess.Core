using Secyud.Ugf.TableComponents;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FlavorTableHelper<TProcess>:TableHelper<TProcess,FlavorCell>
	where TProcess : IFlavorProcess
	{
		protected override void SetCell(FlavorCell cell, TProcess item)
		{
			cell.OnInitialize(item);
		}
	}
}