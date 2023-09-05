using Secyud.Ugf;
using Secyud.Ugf.EditorComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.RoleDomain
{
    public class GenderEditor : EditorBase<Role.BasicProperty>
    {
        [SerializeField] private UnityEvent<string> GenderText;

        public UnityEvent<string> GenderSet => GenderText;

        public void SetGender(bool female)
        {
            Property.Female = female;
            GenderText.Invoke(U.T[female ? "female" : "male"]);
        }
    }
}