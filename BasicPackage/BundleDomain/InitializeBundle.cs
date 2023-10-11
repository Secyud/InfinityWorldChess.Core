using System.Collections.Generic;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BundleDomain
{
    public class InitializeBundle : IBundle
    {
        [field: S] public string ShowDescription { get; set; }
        [field: S] public string ShowName { get; set; }
        [field: S] public IObjectAccessor<Sprite> ShowIcon { get; set; }
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
    }
}