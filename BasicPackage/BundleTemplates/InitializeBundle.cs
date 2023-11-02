using System.Collections.Generic;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BundleTemplates
{
    public class InitializeBundle : IBundle
    {
        [field: S] public string Description { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S] public List<ITrigger> Triggers { get; } = new();

        
        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnGameCreation()
        {
            foreach (ITrigger trigger in Triggers)
            {
                trigger.Invoke();
            }
        }

        public void OnGameLoading()
        {
        }

        public void OnGameSaving()
        {
            
        }
    }
}