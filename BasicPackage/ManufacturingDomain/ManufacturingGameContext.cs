#region

using InfinityWorldChess.WorldDomain;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.Modularity;
using System.Collections.Generic;
using System.IO;
using InfinityWorldChess.PlayerDomain;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
	public class ManufacturingGameContext : ISingleton, IOnGameArchiving
	{
		public List<ManufacturingButtonRegistration> ActivityButtons;

		public void OnGameLoading(LoadingContext context)
		{
			BinaryReader reader = context.GetReader(nameof(ManufacturingGameContext));
			ActivityButtons = new List<ManufacturingButtonRegistration>();
			int count = reader.ReadInt32();
			WorldGameContext wc = GameScope.WorldGameContext;
			for (int i = 0; i < count; i++)
			{
				ManufacturingButtonRegistration tmp = new();
				AddButtonToChecker(tmp, wc.Checkers[reader.ReadInt32()]);
				tmp.Load(reader);
			}
		}

		public void OnGameSaving(SavingContext context)
		{
			BinaryWriter writer = context.GetWriter(nameof(ManufacturingGameContext));
			writer.Write(ActivityButtons.Count);
			foreach (ManufacturingButtonRegistration b in ActivityButtons)
			{
				writer.Write(b.Target.Cell.Index);
				b.Save(writer);
			}
		}

		public void OnGameCreation()
		{
			ActivityButtons = new List<ManufacturingButtonRegistration>();

			WorldGameContext worldContext = GameScope.WorldGameContext;

			int i = 0;

			foreach (WorldChecker checker in worldContext.Checkers)
			{
				if (checker.SpecialIndex != 1) continue;

				for (int j = 0; j < 3; j++)
				{
					ManufacturingButtonRegistration button = new ManufacturingButtonRegistration
					{
						Type = i
					};
					AddButtonToChecker(button, checker);
					i = (i + 1) % 5;
				}
			}
		}

		public void AddButtonToChecker(ManufacturingButtonRegistration button, WorldChecker checker)
		{
			checker.Buttons.Add(button);
			button.Target = checker;
			ActivityButtons.Add(button);
		}
	}
}