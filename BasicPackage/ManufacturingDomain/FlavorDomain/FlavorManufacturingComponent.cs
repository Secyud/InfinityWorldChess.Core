using InfinityWorldChess.Ugf;
using Secyud.Ugf.Layout;
using Secyud.Ugf.TableComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
	public class FlavorManufacturingComponent:MonoBehaviour
	{
		
		public FunctionalTable ProcessesTable;
		public Table AddedProcessesTable;
		public LayoutGroupTrigger ResultContent;
		public NormalCell Process;

		private IFlavorManufacturingData _data;

		public void OnInitialize(IFlavorManufacturingData data)
		{
			_data = data;
		}

		public void OnAddProcessClick()
		{
			_data.OnAddProcessClick();
		}

		public void SetTime(float f)
		{
			_data.SetTime(f);
		}

		public void OnShowResultButtonClick()
		{
			_data.OnShowResultButtonClick();
		}

		public void OnManufactureButtonClick()
		{
			_data.OnManufactureButtonClick();
		}

		public void OnShutdown()
		{
			_data.OnShutdown();
		}

		public void SetName(string text)
		{
			_data.SetName(text);
		}
	}
}