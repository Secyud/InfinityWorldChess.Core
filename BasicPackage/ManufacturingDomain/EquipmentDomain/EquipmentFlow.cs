using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemTemplates;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain.EquipmentDomain
{
    public class EquipmentFlow : ManufactureComponentBase
    {
        [SerializeField] protected LayoutGroupTrigger LayoutGroupTrigger;

        public void GenerateEquipment()
        {
            if (!_checkActions.Values.All(u => u.Invoke()))
                return;
            
            IList<IItem> itemProvider = GetItemProvider();
            itemProvider.Add(GetEquipment());

            foreach (Action action in _clearActions.Values)
                action.Invoke();
            
            "锻造成功。".CreateTipFloatingOnCenter();
        }

        public void PreviewEquipment()
        {
            if (!_checkActions.Values.All(u => u.Invoke()))
                return;

            GetEquipment().SetContent(LayoutGroupTrigger.PrepareLayout());
        }

        private Equipment GetEquipment()
        {
            EquipmentProcessData data = new();

            foreach (Action<EquipmentProcessData> action in _flowActions.Values)
            {
                action.Invoke(data);
            }

            return data.FinishedEquipment();
        }

        private readonly SortedDictionary<int, Action<EquipmentProcessData>> _flowActions = new();

        public void SetFlowAction(int index, Action<EquipmentProcessData> action)
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

        public virtual IList<IItem> GetItemProvider()
        {
            return GameScope.Instance.Player.Role.Item;
        }
    }
}