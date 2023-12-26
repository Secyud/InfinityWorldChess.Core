using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasSetting:MonoBehaviour
    {
        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.worldCamera = U.Camera;
        }
    }
}