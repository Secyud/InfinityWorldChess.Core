#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.WorldDomain;
using System;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.BasicBundle.FormSkills;
using InfinityWorldChess.BasicBundle.Items;
using InfinityWorldChess.BasicBundle.PassiveSkills;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.DependencyInjection;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    [Registry(LifeTime = DependencyLifeTime.Transient)]
    public class RoleGenerator : IRoleGenerator
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
            WorldGameContext worldGameContext
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

                if (U.GetRandom(randomValueMax) == 0)
                    _availableWorldCheckers.Add(checker);
            }

            {
                foreach (string name in resourceManager.CoreSkills)
                {
                    CoreSkillTemplate skill = DataObject.Create<CoreSkillTemplate>(name);
                    int count = GetGenerateCount(skill.Score, 16, resourceManager.CoreSkills.Count);
                    _coreSkills.Add(skill);
                    for (int i = 0; i < count; i++)
                        _coreSkills.Add(skill.Clone() as ICoreSkill);
                }

                _coreSkillCountPerPerson = _coreSkills.Count / GenerateRoleCount;
                Shuffle(_coreSkills);
            }
            {
                foreach (string name in resourceManager.FormSkills)
                {
                    FormSkillTemplate skill = DataObject.Create<FormSkillTemplate>(name);
                    int count = GetGenerateCount(skill.Score, 9, resourceManager.FormSkills.Count);
                    _formSkills.Add(skill);
                    for (int i = 0; i < count; i++)
                        _formSkills.Add(skill.Clone() as IFormSkill);
                }

                _formSkillCountPerPerson = _formSkills.Count / GenerateRoleCount;
                Shuffle(_formSkills);
            }
            {
                foreach (string name in resourceManager.PassiveSkills)
                {
                    PassiveSkillTemplate skill = DataObject.Create<PassiveSkillTemplate>(name);

                    int count = GetGenerateCount(skill.Score, 3, resourceManager.PassiveSkills.Count);
                    _passiveSkills.Add(skill);
                    for (int i = 0; i < count; i++)
                        _passiveSkills.Add(skill.Clone() as IPassiveSkill);
                }

                _passiveSkillCountPerPerson = _passiveSkills.Count / GenerateRoleCount;
                Shuffle(_passiveSkills);
            }
            {
                foreach (string name in resourceManager.Items)
                {
                    ItemTemplate item = DataObject.Create<ItemTemplate>(name);
                    int count = GetGenerateCount(item.Score, 8, resourceManager.Items.Count);
                    _items.Add(item);
                    for (int i = 0; i < count; i++)
                        _items.Add(item.Clone() as IItem);
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
                bool female = U.GetRandom(2) > 0;
                AvatarResourceGroup group = female ? _resourceManager.Female : _resourceManager.Male;
                Role role = new()
                {
                    Basic =
                    {
                        FirstName = _resourceManager.GenerateFirstName(female),
                        LastName = _resourceManager.LastNames.RandomPick(),
                        BirthYear = (byte)U.GetRandom(60),
                        BirthMonth = (byte)U.GetRandom(12),
                        BirthDay = (byte)U.GetRandom(30),
                        BirthHour = (byte)U.GetRandom(12),
                        Female = female,
                        Avatar =
                        {
                            BackItem = new RoleAvatar.AvatarElement { Id = group.BackItem.GetRandomKey() },
                            BackHair = new RoleAvatar.AvatarElement { Id = group.BackHair.GetRandomKey() },
                            Body = new RoleAvatar.AvatarElement { Id = group.Body.GetRandomKey() },
                            Head = new RoleAvatar.AvatarElement { Id = group.Head.GetRandomKey() },
                            HeadFeature =
                                new RoleAvatar.AvatarElement4 { Id = group.HeadFeature.GetRandomKey() },
                            NoseMouth = new RoleAvatar.AvatarElement2X
                            {
                                Id1 =
                                    group.Nose.GetRandomKey(),
                                Id2 = group.Mouth.GetRandomKey()
                            },
                            Eye = new RoleAvatar.AvatarElement4 { Id = group.Eye.GetRandomKey() },
                            Brow = new RoleAvatar.AvatarElement4 { Id = group.Brow.GetRandomKey() },
                            FrontHair = new RoleAvatar.AvatarElement { Id = group.FrontHair.GetRandomKey() },
                        }
                    },
                    Nature =
                    {
                        Recognize = U.GetRandom(1000) - 500,
                        Stability = U.GetRandom(1000) - 500,
                        Confident = U.GetRandom(1000) - 500,
                        Efficient = U.GetRandom(1000) - 500,
                        Gregarious = U.GetRandom(1000) - 500,
                        Altruistic = U.GetRandom(1000) - 500,
                        Rationality = U.GetRandom(1000) - 500,
                        Foresighted = U.GetRandom(1000) - 500,
                        Intelligent = U.GetRandom(1000) - 500,
                    },
                    BodyPart =
                    {
                        Living =
                        {
                            MaxValue = U.GetRandom(100),
                            RealValue = U.GetRandom(100)
                        },
                        Kiling =
                        {
                            MaxValue = U.GetRandom(100) + 1,
                            RealValue = U.GetRandom(100) + 1
                        },
                        Nimble =
                        {
                            MaxValue = U.GetRandom(100) + 1,
                            RealValue = U.GetRandom(100) + 1
                        },
                        Defend =
                        {
                            MaxValue = U.GetRandom(100) + 1,
                            RealValue = U.GetRandom(100) + 1
                        },
                    },
                    Relation =
                    {
                        AreaView = U.GetRandom(500) - 1000,
                        LifeView = U.GetRandom(500) - 1000,
                        ValueView = U.GetRandom(500) - 1000,
                        WorldView = U.GetRandom(500) - 1000,
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
                int r = U.GetRandom(list.Count, i + 1);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        public static int GetGenerateCount(int score, int mult, int totalCount)
        {
            return GenerateRoleCount * (256 - score) * mult / 0x80 / totalCount;
        }
    }
}