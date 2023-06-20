#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.BasicComponents;
using Secyud.Ugf.FunctionalComponents;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	public class NameEditor : MonoBehaviour, ICheckable
	{
		private static readonly string TipTexts = "姓名不可为空且不可超过两个字符。";
		[SerializeField] private SInputField FirstName;
		[SerializeField] private SInputField LastName;

		private Role.BasicProperty _basicProperty;

		private RoleResourceManager _roleResourceManager;
		
		private void Awake()
		{
			_roleResourceManager = Og.DefaultProvider.Get<RoleResourceManager>();
		}

		public bool Check()
		{
			if (_basicProperty.LastName.Length > 2 ||
				_basicProperty.FirstName.Length > 2||
				_basicProperty.FirstName.Length == 0||
				_basicProperty.LastName.Length == 0)
			{
				Og.L[TipTexts].CreateTipFloatingOnCenter();
				return false;
			}

			return true;
		}

		public void SetLastName(string lastName)
		{
			_basicProperty.LastName = lastName;
		}

		public void SetFirstName(string firstName)
		{
			_basicProperty.FirstName = firstName;
		}

		public void SetRandomName()
		{
			FirstName.text = _roleResourceManager.GenerateFirstName(_basicProperty.Female);
			LastName.text = _roleResourceManager.LastNames.RandomPick();
		}

		public void OnInitialize(Role.BasicProperty basicProperty)
		{
			_basicProperty = basicProperty;
			SetRandomName();
		}
	}
}