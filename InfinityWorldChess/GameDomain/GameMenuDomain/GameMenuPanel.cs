using UnityEngine;

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class GameMenuPanel:MonoBehaviour
    {
        public void Die()
        {
            GameScope.Instance.CloseGameMenu();
        }
    }
}