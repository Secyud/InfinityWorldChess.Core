#region

using InfinityWorldChess.ArchivingDomain;
using Secyud.Ugf;
using Secyud.Ugf.ModManagement;
using UnityEngine;

#endregion

namespace InfinityWorldChess.MainMenuDomain
{
    public class MainMenuPanel : MonoBehaviour
    {
        public void OnEnterGameClick()
        {
            ArchivingScope.Instance.OpenGameLoadPanel();
        }


        public void OnSettingsClick()
        {
            U.Log("Settings");
        }

        public void OpenModSetting()
        {
            U.M.CreateScope<SteamModManageScope>();
        }

        public void OnExitGameClick()
        {
            U.Get<MainMenuScope>().ExitGame();
        }
    }
}