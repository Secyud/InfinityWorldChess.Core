namespace InfinityWorldChess.ManufacturingDomain
{
	public interface IEquipmentManufacturingData
	{
		public void OnSelectRawButtonClick();

		public void OnSelectBlueprintButtonClick();

		public void OnShowResultButtonClick();

		public void OnManufactureButtonClick();

		public void OnShutdown();

		public void SetName(string text);
	}
}