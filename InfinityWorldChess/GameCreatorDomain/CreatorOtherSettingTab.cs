using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorOtherSettingTab: TabPanel
    {
        [SerializeField] private PlayerSettingEditor PlayerSettingEditor;
        [SerializeField] private WorldSettingEditor WorldSettingEditor;

        private CreatorTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameCreatorScope.Instance.Get<CreatorTabService>();
            base.Awake();
            PlayerSettingEditor.Bind(GameCreatorScope.Instance.PlayerSetting);
            WorldSettingEditor.Bind(GameCreatorScope.Instance.WorldMessageSetting);
        }

        public override void RefreshTab()
        {
            
        }
    }
}