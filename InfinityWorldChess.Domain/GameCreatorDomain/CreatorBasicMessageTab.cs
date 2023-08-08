using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.RoleDomain.Components;
using UnityEngine;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorBasicMessageTab: MonoBehaviour
    {
        [SerializeField] private NameEditor NameEditor; //2
        [SerializeField] private BirthEditor BirthEditor;
        [SerializeField] private NatureEditor NatureEditor;

        private CreatorTabItem _tabItem;
        private void Awake()
        {
            _tabItem ??= new CreatorTabItem(nameof(CreatorBasicMessageTab), gameObject);
        }

        private  void Start()
        {
            Role role = GameCreatorScope.Instance.Role;

            NameEditor.Bind(role.Basic);
            BirthEditor.Bind(role.Basic);
            NatureEditor.Bind(role.Nature);
        }

        public void SetGender(bool female)
        {
            GameCreatorScope.Instance.Role.Basic.Female = female;
        }
    }
}