using Secyud.Ugf.BasicComponents;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    public class ValueViewer : MonoBehaviour
    {
        [SerializeField] private SText Text;
        [SerializeField] private SSlider Slider;


        public void SetValue(float value, float maxValue)
        {
            Slider.maxValue = maxValue;
            Slider.value = value;
            Text.text = $"{value:F0}/{maxValue:F0}";
        }
    }
}