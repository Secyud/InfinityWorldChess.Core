using System;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
    public class ManufacturingCell : MonoBehaviour
    {
        [SerializeField] private SImage SImage;
        public SImage Image => SImage;

        public int Index { get; set; }

        public Action<ManufacturingCell> ClickAction;
        public Action<ManufacturingCell> HoverAction;
        public Action<ManufacturingCell> RightClickAction;

        public void OnClick()
        {
            ClickAction?.Invoke(this);
        }

        public void OnHover()
        {
            HoverAction?.Invoke(this);
        }

        public void OnRightClick()
        {
            RightClickAction?.Invoke(this);
        }
    }
}