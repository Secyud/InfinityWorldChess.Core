using InfinityWorldChess.RoleDomain;
using Secyud.Ugf.TabComponents;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBasicMessageTab : TabPanel
    {
        [SerializeField] private NameEditor NameEditor; //2
        [SerializeField] private BirthEditor BirthEditor;
        [SerializeField] private NatureEditor NatureEditor;
        [SerializeField] private GenderEditor GenderEditor;

        private CreatorTabService _service;
        protected override TabService Service => _service;

        protected override void Awake()
        {
            _service = GameCreatorScope.Instance.Get<CreatorTabService>();
            base.Awake();
            GameCreatorScope.Instance.Get<CreatorValidateService>().AddObject(
                Name, NameEditor.Check, gameObject);
            RefreshTab();
        }

        public override void RefreshTab()
        {
            Role role = GameCreatorScope.Instance.Role;

            NameEditor.Bind(role.Basic);
            GenderEditor.Bind(role.Basic);
            BirthEditor.Bind(role.Basic);
            NatureEditor.Bind(role.Nature);
        }
    }
}