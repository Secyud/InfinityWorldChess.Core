#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.SkillDomain;
using System;
using System.Collections.Generic;
using System.Ugf.Collections.Generic;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GlobalDomain;
using Secyud.Ugf;
using Secyud.Ugf.Collections;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.HexMap;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public class RoleGenerator : IRegistry
    {
        private const int GenerateRoleCount = 500;
        private readonly RoleResourceManager _resourceManager;
        private readonly List<WorldCell> _availableWorldCheckers = new();
        private readonly List<ICoreSkill> _coreSkills = new();
        private readonly List<IFormSkill> _formSkills = new();
        private readonly List<IPassiveSkill> _passiveSkills = new();
        private readonly List<IItem> _items = new();
        private readonly int _coreSkillCountPerPerson;
        private readonly int _formSkillCountPerPerson;
        private readonly int _passiveSkillCountPerPerson;
        private readonly int _itemCountPerPerson;

        public RoleGenerator(
            RoleResourceManager resourceManager
        )
        {
            _resourceManager = resourceManager;

            foreach (HexCell hexCell in 
                     GameScope.Instance.Map.Value.Cells) 
            {
                WorldCell cell = (WorldCell)hexCell;
                if (cell.SpecialIndex >= 0)
                {
                    _availableWorldCheckers.Add(cell);
                    continue;
                }

                if (cell.IsUnderwater)
                    continue;

                int randomValueMax = 4;

                if (cell.HasRiver)
                    randomValueMax += 2;
                if (cell.HasRoads)
                    randomValueMax -= 1;
                randomValueMax += Math.Max(0, cell.Elevation - 6);
                switch (cell.TerrainType % 4)
                {
                    case 0:
                    case 3:
                        randomValueMax += 2;
                        break;
                }

                switch (cell.TerrainType / 4 % 4)
                {
                    case 0:
                    case 3:
                        randomValueMax += 2;
                        break;
                }

                if (U.GetRandom(randomValueMax) == 0)
                    _availableWorldCheckers.Add(cell);
            }

            {
                foreach ((string name , Guid id) in resourceManager.CoreSkills)
                {
                    if (U.Tm.ConstructFromResource(id,name) is ICoreSkill skill)
                    {
                        int count = GetGenerateCount(skill.Score, 16, resourceManager.CoreSkills.Count);
                        _coreSkills.Add(skill);
                        if (skill is ICloneable c)
                        {
                            for (int i = 0; i < count; i++)
                                _coreSkills.Add(c.Clone() as ICoreSkill);
                        }
                    }
                }

                _coreSkillCountPerPerson = _coreSkills.Count / GenerateRoleCount;
                Shuffle(_coreSkills);
            }
            {
                foreach ((string name , Guid id) in resourceManager.FormSkills)
                {
                    if (U.Tm.ConstructFromResource(id, name) is IFormSkill skill)
                    {
                        int count = GetGenerateCount(skill.Score, 9, resourceManager.FormSkills.Count);
                        _formSkills.Add(skill);
                        if (skill is ICloneable c)
                        {
                            for (int i = 0; i < count; i++)
                                _formSkills.Add(c.Clone() as IFormSkill);
                        }
                    }
                }

                _formSkillCountPerPerson = _formSkills.Count / GenerateRoleCount;
                Shuffle(_formSkills);
            }
            {
                foreach ((string name , Guid id) in resourceManager.PassiveSkills)
                {
                    if (U.Tm.ConstructFromResource(id, name) is IPassiveSkill skill)
                    {
                        int count = GetGenerateCount(skill.Score, 3, resourceManager.PassiveSkills.Count);
                        _passiveSkills.Add(skill);
                        if (skill is ICloneable c)
                        {
                            for (int i = 0; i < count; i++)
                                _passiveSkills.Add(c.Clone() as IPassiveSkill);
                        }
                    }
                }

                _passiveSkillCountPerPerson = _passiveSkills.Count / GenerateRoleCount;
                Shuffle(_passiveSkills);
            }
            {
                foreach ((string name , Guid id) in resourceManager.Items)
                {
                    if (U.Tm.ConstructFromResource(id, name) is IItem item)
                    {
                        int count = GetGenerateCount(item.Score, 8, resourceManager.Items.Count);
                        _items.Add(item);
                        if (item is ICloneable c)
                        {
                            for (int i = 0; i < count; i++)
                                _items.Add(c.Clone() as IItem);
                        }
                    }
                }

                _itemCountPerPerson = _items.Count / GenerateRoleCount;
                Shuffle(_items);
            }
        }

        public IEnumerable<Role> GenerateRole()
        {
            List<Role> roles = new();
            GlobalScope.Instance.RoleContext.CheckMax = true;
            for (int i = 0; i < 500; i++)
            {
                bool female = U.GetRandom(2) > 0;
                RegistrableDictionary<int, AvatarSpriteContainer>[] group =
                    female ? _resourceManager.FemaleAvatarResource : _resourceManager.MaleAvatarResource;
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
                        Female = female
                    },
                    Relation =
                    {
                        AreaView = U.GetRandom(500) - 1000,
                        LifeView = U.GetRandom(500) - 1000,
                        ValueView = U.GetRandom(500) - 1000,
                        WorldView = U.GetRandom(500) - 1000,
                    }
                };

                for (int j = 0; j < SharedConsts.AvatarElementCount; j++)
                {
                    role.Basic.Avatar[j] = new AvatarElement
                    {
                        Id = group[j].GetRandomKey(),
                        // TODO: logic change
                        // PositionX = (byte)(U.GetRandom(127) + 64),
                        // PositionY = (byte)(U.GetRandom(127) + 64),
                        // Scale = (byte)(U.GetRandom(127) + 64),
                        // Rotation = (byte)(U.GetRandom(127) + 64)
                    };
                }

                for (int j = 0; j < 9; j++)
                {
                    role.Nature.Properties[j] = U.GetRandom(1000) - 500;
                }

                for (int j = 0; j < 4; j++)
                {
                    RoleBodyPart body = role.BodyPart[(BodyType)j];
                    body.MaxValue = U.GetRandom(100) + 1;
                    body.RealValue = body.MaxValue;
                }

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
                ICoreSkill skill = _coreSkills[i];
                role.CoreSkill.LearnedSkills[skill.ShowName] = skill;
                _coreSkills.RemoveAt(i);
            }

            total = _formSkills.Count;
            min = Math.Max(total - _formSkillCountPerPerson, 0);
            for (int i = total - 1; i >= min; i--)
            {
                IFormSkill skill = _formSkills[i];
                role.FormSkill.LearnedSkills[skill.ShowName] = skill;
                _formSkills.RemoveAt(i);
            }

            total = _passiveSkills.Count;
            min = Math.Max(total - _passiveSkillCountPerPerson, 0);
            for (int i = total - 1; i >= min; i--)
            {
                IPassiveSkill skill = _passiveSkills[i];
                role.PassiveSkill.LearnedSkills[skill.ShowName] = skill;
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