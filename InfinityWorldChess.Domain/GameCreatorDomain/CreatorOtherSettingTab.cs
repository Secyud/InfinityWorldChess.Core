using InfinityWorldChess.PlayerDomain;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorOtherSettingTab: MonoBehaviour
    {


        [SerializeField] private PlayerSettingEditor PlayerSettingEditor;
        [SerializeField] private WorldSettingEditor WorldSettingEditor;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorOtherSettingTab), gameObject);
        }
        protected void Start()
        {
            PlayerSettingEditor.Bind(GameCreatorScope.Instance.PlayerSetting);
            WorldSettingEditor.Bind(GameCreatorScope.Instance.WorldSetting);
        }
    }
}