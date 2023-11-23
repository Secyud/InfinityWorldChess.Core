using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.RoleDomain
{
    public class RoleTemplate
    {
        [field: S(-1)] public int Id { get; set; }
        [field: S(0)] public int PositionIndex { get; set; }
        [field: S(1)] public string LastName { get; set; }
        [field: S(2)] public string FirstName { get; set; }
        [field: S(3)] public string Description { get; set; }
        [field: S(4)] public int BirthYear { get; set; }
        [field: S(5)] public byte BirthMonth { get; set; }
        [field: S(6)] public byte BirthDay { get; set; }
        [field: S(7)] public byte BirthHour { get; set; }
        [field: S(8)] public bool Female { get; set; }

        [field: S(9)]
        public AvatarElement[] Avatar { get; } =
            new AvatarElement[SharedConsts.AvatarElementCount];

        [field: S(10)] public int Living { get; set; }
        [field: S(10)] public int Kiling { get; set; }
        [field: S(10)] public int Nimble { get; set; }
        [field: S(10)] public int Defend { get; set; }
        [field: S(10)] public byte SkillPoint { get; set; }


        [field: S(11)] public List<IObjectAccessor<ICoreSkill>> LearnedCoreSkills { get; } = new();

        [field: S(12)]
        public IObjectAccessor<ICoreSkill>[] CoreSkills { get; } =
            new IObjectAccessor<ICoreSkill>[SharedConsts.CoreSkillCount];

        [field: S(13)] public List<IObjectAccessor<IFormSkill>> LearnedFormSkills { get; } = new();

        [field: S(14)]
        public IObjectAccessor<IFormSkill>[] FormSkills { get; } =
            new IObjectAccessor<IFormSkill>[SharedConsts.FormSkillCount];

        [field: S(15)] public List<IObjectAccessor<IPassiveSkill>> LearnedPassiveSkills { get; } = new();

        [field: S(16)]
        public IObjectAccessor<IPassiveSkill>[] PassiveSkills { get; } =
            new IObjectAccessor<IPassiveSkill>[SharedConsts.PassiveSkillCount];

        [field: S(17)] public List<IObjectAccessor<IItem>> Items { get; } = new();

        [field: S(18)]
        public IObjectAccessor<IEquipment>[] Equipments { get; } =
            new IObjectAccessor<IEquipment>[SharedConsts.MaxBodyPartsCount];

        // 认知
        [field: S(19)] public float Recognize { get; set; }

        // 稳定 
        [field: S(19)] public float Stability { get; set; }

        // 能力 
        [field: S(19)] public float Confident { get; set; }

        // 效益
        [field: S(19)] public float Efficient { get; set; }

        // 合群
        [field: S(20)] public float Gregarious { get; set; }

        // 利他
        [field: S(20)] public float Altruistic { get; set; }

        // 理性 
        [field: S(21)] public float Rationality { get; set; }

        // 远见
        [field: S(21)] public float Foresighted { get; set; }

        // 渊博
        [field: S(21)] public float Intelligent { get; set; }

        [field: S(22)] public float AreaView { get; set; }
        [field: S(22)] public float LifeView { get; set; }

        [field: S(22)] public float ValueView { get; set; }
        [field: S(22)] public float WorldView { get; set; }
        [field: S(23)] public List<IObjectAccessor<IBuff<Role>>> RoleBuffs { get; } = new();
        [field: S(24)] public List<IObjectAccessor<RoleProperty>> ExtraProperties { get; } = new();


        public Role GenerateRole()
        {
            Role role = new()
            {
                Id = Id,
                Basic =
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Female = Female,
                    BirthYear = BirthYear,
                    BirthMonth = BirthMonth,
                    BirthDay = BirthDay,
                    BirthHour = BirthHour,
                    Description = Description
                },
                BodyPart =
                {
                    Living = { MaxValue = Living, RealValue = Living },
                    Kiling = { MaxValue = Kiling, RealValue = Kiling },
                    Nimble = { MaxValue = Nimble, RealValue = Nimble },
                    Defend = { MaxValue = Defend, RealValue = Defend },
                },
                Nature =
                {
                    Recognize = Recognize, Stability = Stability, Confident = Confident, Efficient = Efficient,
                    Gregarious = Gregarious, Altruistic = Altruistic,
                    Foresighted = Foresighted, Rationality = Rationality, Intelligent = Intelligent
                },
                Relation = { AreaView = AreaView, LifeView = LifeView, WorldView = WorldView, ValueView = ValueView },
            };

            for (int i = 0; i < Avatar.Length; i++)
            {
                AvatarElement element = Avatar[i];
                role.Basic.Avatar[i] = element ?? new AvatarElement();
            }
            byte living, kiling, nimble, defend;

            {
                float totalValue = Living + Kiling + Nimble + Defend;
                living = (byte)(SkillPoint * Living / totalValue);
                kiling = (byte)(SkillPoint * Kiling / totalValue);
                nimble = (byte)(SkillPoint * Nimble / totalValue);
                defend = (byte)(SkillPoint * Defend / totalValue);
            }
            
            
            foreach (IObjectAccessor<ICoreSkill> accessor in LearnedCoreSkills)
            {
                var skill = accessor?.Value;
                if (skill is null)continue;
                SetSkill(skill);
                role.CoreSkill.TryAddLearnedSkill(skill);
            }

            for (int i = 0; i < CoreSkills.Length; i++)
            {
                var accessor = CoreSkills[i];
                var skill = accessor?.Value;
                if (skill is null)continue;
                SetSkill(skill);
                role.CoreSkill.TryAddLearnedSkill(skill);
                Role.CoreSkillProperty.GetCodeAndLayer(i, out byte layer, out byte code);
                role.CoreSkill.Set(skill, layer, code);
            }

            foreach (IObjectAccessor<IFormSkill> accessor in LearnedFormSkills)
            {
                var skill = accessor?.Value;
                SetSkill(skill);
                role.FormSkill.TryAddLearnedSkill(skill);
            }

            for (int i = 0; i < FormSkills.Length; i++)
            {
                var accessor = FormSkills[i];
                var skill = accessor?.Value;
                if (skill is null)continue;
                SetSkill(skill);
                role.FormSkill.TryAddLearnedSkill(skill);
                role.FormSkill.Set(skill, (byte)(i / SharedConsts.FormSkillTypeCount),
                    (byte)(i % SharedConsts.FormSkillTypeCount));
            }

            foreach (IObjectAccessor<IPassiveSkill> accessor in LearnedPassiveSkills)
            {
                var skill = accessor?.Value;
                if (skill is null)continue;
                SetSkill(skill);
                role.PassiveSkill.TryAddLearnedSkill(skill);
            }

            for (int i = 0; i < PassiveSkills.Length; i++)
            {
                var accessor = PassiveSkills[i];
                var skill = accessor?.Value;
                if (skill is null)continue;
                role.PassiveSkill.TryAddLearnedSkill(skill);
                role.PassiveSkill[i, role] = skill;
            }

            foreach (IObjectAccessor<IItem> accessor in Items)
            {
                var item = accessor?.Value;
                if (item is null)continue;
                role.Item.Add(accessor.Value);
            }

            for (int i = 0; i < Equipments.Length; i++)
            {
                var equipmentAccessor = Equipments[i];
                IEquipment equipment = equipmentAccessor?.Value;
                if (equipment is null)continue;
                role.Item.Add(equipment);
                role.Equipment[(byte)i, role] = equipment;
            }

            foreach (IObjectAccessor<RoleProperty> extraProperty in ExtraProperties)
            {
                role.ReplaceProperty(extraProperty.Value);
            }

            foreach (IObjectAccessor<IBuff<Role>> buff in RoleBuffs)
            {
                role.IdBuffs.Install(buff.Value);
            }
            
            return role;

            void SetSkill(ISkill skill)
            {
                skill.Living = living;
                skill.Kiling = kiling;
                skill.Nimble = nimble;
                skill.Defend = defend;
            }
        }
    }
}