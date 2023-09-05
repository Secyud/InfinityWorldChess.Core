using UnityEngine;

namespace InfinityWorldChess.ArchivingDomain
{
    public class GameLoadPanel:MonoBehaviour
    {
        public void CloseGameLoadPanel()
        {
            ArchivingScope.Instance.CloseGameLoadPanel();
        }
    }
}