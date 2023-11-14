using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class ValueViewer : MonoBehaviour
    {
        [SerializeField] private SText Text;
        [SerializeField] private SSlider Slider;


        public void SetValue(float value, float maxValue)
        {
            if (Slider)
            {
                Slider.maxValue = maxValue;
                Slider.value = value;
            }
            if (Text)
            {
                Text.text = $"{value:F0}/{maxValue:F0}";
            }
        }
    }
}