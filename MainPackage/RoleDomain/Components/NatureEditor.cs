#region

using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class NatureEditor : EditorBase<Role.NatureProperty>
    {
        [SerializeField] private EditorEvent<string> Recognize;
        [SerializeField] private EditorEvent<string> Stability;
        [SerializeField] private EditorEvent<string> Confident;
        [SerializeField] private EditorEvent<string> Efficient;
        [SerializeField] private EditorEvent<string> Gregarious;
        [SerializeField] private EditorEvent<string> Altruistic;
        [SerializeField] private EditorEvent<string> Rationality;
        [SerializeField] private EditorEvent<string> Intelligent;
        [SerializeField] private EditorEvent<string> Foresighted;

        public void SetRecognize(float value)
        {
            Property.Recognize = value;
            Recognize.Invoke(Property.Recognize.ToString("0"));
        }

        public void SetStability(float value)
        {
            Property.Stability = value;
            Stability.Invoke(Property.Stability.ToString("0"));
        }

        public void SetConfident(float value)
        {
            Property.Confident = value;
            Confident.Invoke(Property.Confident.ToString("0"));
        }

        public void SetEfficient(float value)
        {
            Property.Efficient = value;
            Efficient.Invoke(Property.Efficient.ToString("0"));
        }

        public void SetGregarious(float value)
        {
            Property.Gregarious = value;
            Gregarious.Invoke(Property.Gregarious.ToString("0"));
        }

        public void SetAltruistic(float value)
        {
            Property.Altruistic = value;
            Altruistic.Invoke(Property.Altruistic.ToString("0"));
        }

        public void SetRationality(float value)
        {
            Property.Rationality = value;
            Rationality.Invoke(Property.Rationality.ToString("0"));
        }

        public void SetIntelligent(float value)
        {
            Property.Intelligent = value;
            Intelligent.Invoke(Property.Intelligent.ToString("0"));
        }

        public void SetForesighted(float value)
        {
            Property.Foresighted = value;
            Foresighted.Invoke(Property.Foresighted.ToString("0"));
        }

        protected override void InitData()
        {
            Recognize.Invoke(Property.Recognize.ToString("0"));
            Stability.Invoke(Property.Stability.ToString("0"));
            Confident.Invoke(Property.Confident.ToString("0"));
            Efficient.Invoke(Property.Efficient.ToString("0"));
            Gregarious.Invoke(Property.Gregarious.ToString("0"));
            Altruistic.Invoke(Property.Altruistic.ToString("0"));
            Rationality.Invoke(Property.Rationality.ToString("0"));
            Intelligent.Invoke(Property.Intelligent.ToString("0"));
            Foresighted.Invoke(Property.Foresighted.ToString("0"));
        }
    }
}