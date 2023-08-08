using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
    public abstract class ManufactureComponentBase : MonoBehaviour
    {
        public Manufacture Manufacture { get; internal set; }
        public virtual string Name => GetType().Name;

        public virtual void OnInitialize()
        {
        }
    }
}