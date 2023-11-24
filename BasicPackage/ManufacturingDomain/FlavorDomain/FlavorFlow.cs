using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.FlavorDomain
{
    public abstract class FlavorFlow<TProcessData,TFlavor>: ManufactureComponentBase
    where TProcessData : FlavorProcessData, new()
    where TFlavor : IItem
    {
        [SerializeField] protected LayoutGroupTrigger LayoutGroupTrigger;

        public void GenerateFlavor()
        {
            if (!_checkActions.Values.All(u => u.Invoke()))
                return;
            
            Role.ItemProperty itemProvider = GetItemProvider();
            
            itemProvider.Add(GetFlavor());

            foreach (Action action in _clearActions.Values)
            {
                action.Invoke();
            }
            
            "制造成功。".CreateTipFloatingOnCenter();
        }

        public void PreviewFlavor()
        {
            if (!_checkActions.Values.All(u => u.Invoke()))
                return;

            GetFlavor().SetContent(LayoutGroupTrigger.PrepareLayout());
        }

        private IItem GetFlavor()
        {
            TProcessData data = new();

            foreach (Action<TProcessData> action in _flowActions.Values)
            {
                action.Invoke(data);
            }

            return data.FinishedFlavor();
        }

        private readonly SortedDictionary<int, Action<TProcessData>> _flowActions = new();

        public void SetFlowAction(int index, Action<TProcessData> action)
        {
            _flowActions[index] = action;
        }

        private readonly SortedDictionary<int, Action> _clearActions = new();

        public void SetClearAction(int index, Action action)
        {
            _clearActions[index] = action;
        }

        private readonly SortedDictionary<int, Func<bool>> _checkActions = new();

        public void SetCheckAction(int index, Func<bool> action)
        {
            _checkActions[index] = action;
        }

        public virtual Role.ItemProperty GetItemProvider()
        {
            return GameScope.Instance.Player.Role.Item;
        }
        


        protected void Manufacturing(FlavorProcessBase<TProcessData> process,TProcessData data)
        {
            process.Process(Manufacture,data);
        }
    }
}