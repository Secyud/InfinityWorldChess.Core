using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldCellDomain
{
    public class CurrentFunctionsTab : TabPanel
    {
        [SerializeField] private SButtonGroup ButtonGroup;

        private CurrentTabService _service; 
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameScope.Instance.Get<CurrentTabService>();
            base.Awake();
        }

        public override void RefreshTab()
        {
            ButtonGroup.Clear();

            WorldCell cell = _service.Cell;
            if (cell is not null )
            {
                ButtonGroup.OnInitialize(cell, cell.Buttons);
            }
        }
    }
}