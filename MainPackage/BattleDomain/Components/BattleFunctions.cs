using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.BattleDomain
{
    public class BattleFunctions:MonoBehaviour
    {
        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.worldCamera = U.Camera;
        }
    }
}