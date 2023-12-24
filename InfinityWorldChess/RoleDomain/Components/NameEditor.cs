#region

using System.Ugf.Collections.Generic;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class NameEditor : EditorBase<Role.BasicProperty>
    {
        private static readonly string TipTexts = "姓名不可为空且不可超过两个字符。";

        [SerializeField] private EditorEvent<string> FirstNameText;
        [SerializeField] private EditorEvent<string> LastNameText;

        private RoleResourceManager _resource;
        private RoleResourceManager Resource => _resource ??= U.Get<RoleResourceManager>();


        public bool Check()
        {
            if (Property.LastName.Length is <= 2 and > 0 &&
                Property.FirstName.Length is <= 2 and > 0)
                return true;

            U.T[TipTexts].CreateTipFloatingOnCenter();
            return false;
        }

        public void SetLastName(string lastName)
        {
            Property.LastName = lastName;
            LastNameText.Invoke(lastName);
        }

        public void SetFirstName(string firstName)
        {
            Property.FirstName = firstName;
            FirstNameText.Invoke(firstName);
        }

        public void SetRandomName()
        {
            SetFirstName(Resource.GenerateFirstName(Property.Female));
            SetLastName(Resource.LastNames.Items.RandomPick());
        }

        protected override void InitData()
        {
        }
    }
}