#region

using InfinityWorldChess.SkillDomain.SkillBrowseDomain;
using Secyud.Ugf.BasicComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.GameDomain.GameMenuDomain
{
    public class RoleMessageComponent : MonoBehaviour
    {
        [SerializeField] private SkillBrowseTabs SkillBrowseTabs;
        [SerializeField] private SButton[] PageButtons;
       

        public void Die()
        {
            GameScope.CloseGameMenu();
        }

        public void SetPage(int page)
        {
            if (page >= PageButtons.Length)
                return;

            PageButtons[page].onClick.Invoke();
        }


    }
}