using System.Collections.Generic;
using System.Runtime.InteropServices;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BundleTemplates
{
    [Guid("E952883B-4090-D695-157C-3E47DCB61D00")]
    public class InitializeBundle : IBundle
    {
        [field: S] public string Description { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S] public List<IActionable> Triggers { get; } = new();

        
        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnGameCreation()
        {
            foreach (IActionable trigger in Triggers)
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