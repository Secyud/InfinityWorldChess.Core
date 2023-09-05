using System;
using UnityEngine;

namespace InfinityWorldChess.GameDomain.WorldMapDomain
{
    public class WorldUi:MonoBehaviour
    {
        [SerializeField] private bool DestroyableItem;

        public bool Destroyable => DestroyableItem;
        
        private void Awake()
        {
            GameScope.Instance.WorldUis.Add(this);
        }

        private void OnDestroy()
        {
            if (GameScope.Instance.Operability)
                GameScope.Instance.WorldUis.Remove(this);
        }
    }
}