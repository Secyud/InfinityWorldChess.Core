#region

using System.Globalization;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain.Components
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
            Recognize.Invoke(Property.Recognize.ToString(CultureInfo.InvariantCulture));
        }

        public void SetStability(float value)
        {
            Property.Stability = value;
            Stability.Invoke(Property.Stability.ToString(CultureInfo.InvariantCulture));
        }

        public void SetConfident(float value)
        {
            Property.Confident = value;
            Confident.Invoke(Property.Confident.ToString(CultureInfo.InvariantCulture));
        }

        public void SetEfficient(float value)
        {
            Property.Efficient = value;
            Efficient.Invoke(Property.Efficient.ToString(CultureInfo.InvariantCulture));
        }

        public void SetGregarious(float value)
        {
            Property.Gregarious = value;
            Gregarious.Invoke(Property.Gregarious.ToString(CultureInfo.InvariantCulture));
        }

        public void SetAltruistic(float value)
        {
            Property.Altruistic = value;
            Altruistic.Invoke(Property.Altruistic.ToString(CultureInfo.InvariantCulture));
        }

        public void SetRationality(float value)
        {
            Property.Rationality = value;
            Rationality.Invoke(Property.Rationality.ToString(CultureInfo.InvariantCulture));
        }

        public void SetIntelligent(float value)
        {
            Property.Intelligent = value;
            Intelligent.Invoke(Property.Intelligent.ToString(CultureInfo.InvariantCulture));
        }

        public void SetForesighted(float value)
        {
            Property.Foresighted = value;
            Foresighted.Invoke(Property.Foresighted.ToString(CultureInfo.InvariantCulture));
        }

        protected override void InitData()
        {
            Recognize.Invoke(Property.Recognize.ToString(CultureInfo.InvariantCulture));
            Stability.Invoke(Property.Stability.ToString(CultureInfo.InvariantCulture));
            Confident.Invoke(Property.Confident.ToString(CultureInfo.InvariantCulture));
            Efficient.Invoke(Property.Efficient.ToString(CultureInfo.InvariantCulture));
            Gregarious.Invoke(Property.Gregarious.ToString(CultureInfo.InvariantCulture));
            Altruistic.Invoke(Property.Altruistic.ToString(CultureInfo.InvariantCulture));
            Rationality.Invoke(Property.Rationality.ToString(CultureInfo.InvariantCulture));
            Intelligent.Invoke(Property.Intelligent.ToString(CultureInfo.InvariantCulture));
            Foresighted.Invoke(Property.Foresighted.ToString(CultureInfo.InvariantCulture));
        }
    }
}