using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ManufacturingDomain.Components;
using InfinityWorldChess.Ugf;

namespace InfinityWorldChess.ManufacturingDomain.FoodDomain
{
    public class FoodRawSelect :
        Select<IItem, ItemSorters, ItemFilters>
    {
        protected override IList<IItem> GetSelectList()
        {
            return GameScope.Instance.Player.Role.Item.Where(u => u is FoodRaw).ToList();
        }
        
        
        public override void OnInitialize()
        {
            FoodFlow flow = Manufacture.Get<FoodFlow>();
            flow.SetClearAction(0, ClearBoard);
            flow.SetCheckAction(0, CheckRaw);
            flow.SetFlowAction(0, BeginFlow);
        }

        private void ClearBoard()
        {
            GameScope.Instance.Player.Role.Item.Remove(SelectedItem);
            ChangeSelect(null);
        }

        private bool CheckRaw()
        {
            if (SelectedItem is not null)
                return true;
            "请选择主要材料".CreateTipFloatingOnCenter();
            return false;
        }

        private void BeginFlow(FoodProcessData data)
        {
            FoodRaw raw = SelectedItem as FoodRaw;
            for (int i = 0; i < BasicConsts.FlavorCount; i++)
            {
                data.MouthFeelLevel[i] = raw!.MouthFeelLevel[i];
            }
            raw!.Init(data);
        }
    }
}