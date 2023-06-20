#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.WorldDomain;
using Secyud.Ugf;
using Secyud.Ugf.DependencyInjection;
using System;
using System.Collections.Generic;
using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.BasicBundle.FormSkills;
using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.BasicBundle.PassiveSkills;
using Secyud.Ugf.DataManager;

#endregion

namespace InfinityWorldChess.RoleDomain
{
	[ExposeType(typeof(IRoleGenerator))]
	public class RoleGenerator : IRoleGenerator, ITransient
	{
		private const int GenerateRoleCount = 500;
		private readonly RoleResourceManager _resourceManager;
		private readonly List<WorldChecker> _availableWorldCheckers = new();
		private readonly List<ICoreSkill> _coreSkills = new();
		private readonly List<IFormSkill> _formSkills = new();
		private readonly List<IPassiveSkill> _passiveSkills = new();
		private readonly List<IItem> _items = new();
		private readonly int _coreSkillCountPerPerson;
		private readonly int _formSkillCountPerPerson;
		private readonly int _passiveSkillCountPerPerson;
		private readonly int _itemCountPerPerson;

		public RoleGenerator(
			RoleResourceManager resourceManager,
			WorldGameContext worldGameContext,
			InitializeManager initializeManager
		)
		{
			_resourceManager = resourceManager;

			foreach (WorldChecker checker in worldGameContext.Checkers)
			{
				if (checker.SpecialIndex >= 0)
				{
					_availableWorldCheckers.Add(checker);
					continue;
				}
				if (checker.Cell.IsUnderwater)
					continue;

				int randomValueMax = 4;

				if (checker.Cell.HasRiver)
					randomValueMax += 2;
				if (checker.Cell.HasRoads)
					randomValueMax -= 1;
				randomValueMax += Math.Max(0, checker.Cell.Elevation - 6);
				switch (checker.Cell.TerrainTypeIndex % 4)
				{
				case 0:
				case 3:
					randomValueMax += 2;
					break;
				}
				switch (checker.Cell.TerrainTypeIndex / 4 % 4)
				{
				case 0:
				case 3:
					randomValueMax += 2;
					break;
				}
				if (Og.GetRandom(randomValueMax) == 0)
					_availableWorldCheckers.Add(checker);
			}


			{
				List<CoreSkillTemplate> skills = 
					resourceManager.CoreSkills.CreateAndInitList<CoreSkillTemplate>();
				foreach (ICoreSkill skill in skills)
				{
					int count = GetGenerateCount(skill.Score, 16, skills.Count);
					_coreSkills.AddRange(skills);
					if (skill is ICloneable cloneable)
						for (int i = 0; i < count; i++)
							_coreSkills.Add(cloneable.Clone() as ICoreSkill);
				}
				_coreSkillCountPerPerson = _coreSkills.Count / GenerateRoleCount;
				Shuffle(_coreSkills);
			}
			{
				List<FormSkillTemplate> skills = 
					resourceManager.FormSkills.CreateAndInitList<FormSkillTemplate>();
				foreach (IFormSkill skill in skills)
				{
					int count = GetGenerateCount(skill.Score, 9, skills.Count);
					_formSkills.AddRange(skills);
					if (skill is ICloneable cloneable)
						for (int i = 0; i < count; i++)
							_formSkills.Add(cloneable.Clone() as IFormSkill);
				}
				_formSkillCountPerPerson = _formSkills.Count / GenerateRoleCount;
				Shuffle(_formSkills);
			}
			{
				List<PassiveSkillTemplate> skills = 
					resourceManager.PassiveSkills.CreateAndInitList<PassiveSkillTemplate>();
				foreach (IPassiveSkill skill in skills)
				{
					int count = GetGenerateCount(skill.Score, 3, skills.Count);
					_passiveSkills.AddRange(skills);
					if (skill is ICloneable cloneable)
						for (int i = 0; i < count; i++)
							_passiveSkills.Add(cloneable.Clone() as IPassiveSkill);
				}
				_passiveSkillCountPerPerson = _passiveSkills.Count / GenerateRoleCount;
				Shuffle(_passiveSkills);
			}
			{
				List<ItemTemplate> items = 
					resourceManager.Items.CreateAndInitList<ItemTemplate>();
				foreach (IItem item in items)
				{
					int count = GetGenerateCount(item.Score, 8, items.Count);
					_items.AddRange(items);
					if (item is ICloneable cloneable)
						for (int i = 0; i < count; i++)
							_items.Add(cloneable.Clone() as IItem);
				}
				_itemCountPerPerson = _items.Count / GenerateRoleCount;
				Shuffle(_items);
			}
		}

		public IEnumerable<Role> GenerateRole()
		{
			List<Role> roles = new();

			for (int i = 0; i < 500; i++)
			{
				bool female = Og.GetRandom(2) > 0;
				AvatarResourceGroup group = female ? _resourceManager.Female : _resourceManager.Male;
				Role role = new()
				{
					Basic =
					{
						FirstName = _resourceManager.GenerateFirstName(female),
						LastName = _resourceManager.LastNames.RandomPick(),
						BirthYear = (byte)Og.GetRandom(60),
						BirthMonth = (byte)Og.GetRandom(12),
						BirthDay = (byte)Og.GetRandom(30),
						BirthHour = (byte)Og.GetRandom(12),
						Female = female,
						Avatar =
						{
							BackItem = new RoleAvatar.AvatarElement(group.BackItem.GetRandomKey()),
							BackHair = new RoleAvatar.AvatarElement(group.BackHair.GetRandomKey()),
							Body = new RoleAvatar.AvatarElement(group.Body.GetRandomKey()),
							Head = new RoleAvatar.AvatarElement(group.Head.GetRandomKey()),
							HeadFeature =
								new RoleAvatar.AvatarElement4(group.HeadFeature.GetRandomKey()),
							NoseMouth = new RoleAvatar.AvatarElement2X(
								group.Nose.GetRandomKey(), group.Mouth.GetRandomKey()
							),
							Eye = new RoleAvatar.AvatarElement4(group.Eye.GetRandomKey()),
							Brow = new RoleAvatar.AvatarElement4(group.Brow.GetRandomKey()),
							FrontHair = new RoleAvatar.AvatarElement(group.FrontHair.GetRandomKey()),
						}
					},
					Nature =
					{
						Recognize = Og.GetRandom(1000) - 500,
						Stability = Og.GetRandom(1000) - 500,
						Confident = Og.GetRandom(1000) - 500,
						Efficient = Og.GetRandom(1000) - 500,
						Gregarious = Og.GetRandom(1000) - 500,
						Altruistic = Og.GetRandom(1000) - 500,
						Rationality = Og.GetRandom(1000) - 500,
						Foresighted = Og.GetRandom(1000) - 500,
						Intelligent = Og.GetRandom(1000) - 500,
					},
					BodyPart =
					{
						Living =
						{
							MaxValue = Og.GetRandom(100),
							RealValue = Og.GetRandom(100)
						},
						Kiling =
						{
							MaxValue = Og.GetRandom(100) + 1,
							RealValue = Og.GetRandom(100) + 1
						},
						Nimble =
						{
							MaxValue = Og.GetRandom(100) + 1,
							RealValue = Og.GetRandom(100) + 1
						},
						Defend =
						{
							MaxValue = Og.GetRandom(100) + 1,
							RealValue = Og.GetRandom(100) + 1
						},
					},
					Relation =
					{
						AreaView = Og.GetRandom(500) - 1000,
						LifeView = Og.GetRandom(500) - 1000,
						ValueView = Og.GetRandom(500) - 1000,
						WorldView = Og.GetRandom(500) - 1000,
					}
				};
				GenerateSkill(role);
				GenerateItemAndEquipment(role);
				AddToWorld(role);
				roles.Add(role);
			}

			return roles;
		}

		public void GenerateSkill(Role role)
		{
			int total = _coreSkills.Count;
			int min = Math.Max(total - _coreSkillCountPerPerson, 0);
			for (int i = total - 1; i >= min; i--)
			{
				role.CoreSkill.LearnedSkills.Add(_coreSkills[i]);
				_coreSkills.RemoveAt(i);
			}
			total = _formSkills.Count;
			min = Math.Max(total - _formSkillCountPerPerson, 0);
			for (int i = total - 1; i >= min; i--)
			{
				role.FormSkill.LearnedSkills.Add(_formSkills[i]);
				_formSkills.RemoveAt(i);
			}
			total = _passiveSkills.Count;
			min = Math.Max(total - _passiveSkillCountPerPerson, 0);
			for (int i = total - 1; i >= min; i--)
			{
				role.PassiveSkill.LearnedSkills.Add(_passiveSkills[i]);
				_passiveSkills.RemoveAt(i);
			}

			role.AutoEquipCoreSkill();
			role.AutoEquipFormSkill();
			role.AutoEquipPassiveSkill();
		}

		public void GenerateItemAndEquipment(Role role)
		{
			int total = _items.Count;
			int min = Math.Max(total - _itemCountPerPerson, 0);
			for (int i = total - 1; i >= min; i--)
			{
				role.Item.Add(_items[i]);
				_items.RemoveAt(i);
			}
			role.AutoEquipEquipment();
		}

		public void AddToWorld(Role role)
		{
			role.Position = _availableWorldCheckers.RandomPick();
		}

		public static void Shuffle<T>(List<T> list)
		{
			int count = list.Count - 1;
			for (int i = 0; i < count; i++)
			{
				int r = Og.GetRandom(list.Count, i + 1);
				(list[i], list[r]) = (list[r], list[i]);
			}
		}

		public static int GetGenerateCount(int score, int mult, int totalCount)
		{
			return GenerateRoleCount * (256 - score) * mult / 0x80 / totalCount;
		}
	}
}