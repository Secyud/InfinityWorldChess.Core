#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.Layout;
using Secyud.Ugf.TableComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public class EquipmentManufacturingComponent : MonoBehaviour
	{
		public FunctionalTable ProcessesTable;
		public LayoutGroupTrigger CellContent;
		public LayoutGroupTrigger ResultContent;
		public NormalCell Raw;
		public NormalCell Blueprint;
		public NormalCell Process;
		private IEquipmentManufacturingData _data;

		public void OnInitialize(IEquipmentManufacturingData data)
		{
			_data = data;
		}

		public void OnSelectRawButtonClick()
		{
			_data.OnSelectRawButtonClick();
		}

		public void OnShowResultButtonClick()
		{
			_data.OnShowResultButtonClick();
		}

		public void OnManufactureButtonClick()
		{
			_data.OnManufactureButtonClick();
		}

		public void OnSelectBlueprintButtonClick()
		{
			_data.OnSelectBlueprintButtonClick();
		}

		public void OnShutdown()
		{
			_data.OnShutdown();
		}

		public void SetEquipmentName(string text)
		{
			_data.SetName(text);
		}
	}
}