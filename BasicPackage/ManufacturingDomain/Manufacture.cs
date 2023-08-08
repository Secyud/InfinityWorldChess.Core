using System.Collections.Generic;
using UnityEngine;

namespace InfinityWorldChess.ManufacturingDomain
{
    public class Manufacture : ManufactureComponentBase
    {
        [SerializeField] private ManufactureComponentBase[] Components;

        private Dictionary<string, ManufactureComponentBase> _componentDict;
        public override string Name => nameof(Manufacture);

        public ManufactureComponentBase this[string componentName]
        {
            get
            {
                _componentDict.TryGetValue(componentName, out ManufactureComponentBase v);
                return v;
            }
        }

        public TComponent Get<TComponent>()
            where TComponent : ManufactureComponentBase
        {
            return this[typeof(TComponent).Name] as TComponent;
        }

        private void Awake()
        {
            _componentDict = new Dictionary<string, ManufactureComponentBase>();
            foreach (ManufactureComponentBase component in Components)
            {
                if (!component) continue;
                _componentDict[component.Name] = component;
                component.Manufacture = this;
            }

            foreach (ManufactureComponentBase component in Components)
            {
                if (!component) continue;
                component.OnInitialize();
            }
        }
    }
}