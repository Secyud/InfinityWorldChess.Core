using System.Collections.Generic;
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
        [field: S(0)] public int PositionX { get; set; }
        [field: S(0)] public int PositionZ { get; set; }
        [field: S(1)] public string LastName { get; set; }
        [field: S(2)] public string FirstName { get; set; }
        [field: S(3)] public string Description { get; set; }
        [field: S(4)] public int BirthYear { get; set; }
        [field: S(5)] public byte BirthMonth { get; set; }
        [field: S(6)] public byte BirthDay { get; set; }
        [field: S(7)] public byte BirthHour { get; set; }
        [field: S(8)] public bool Female { get; set; }

        [field: S(16)]
        public AvatarElement[] Avatar { get; } =
            new AvatarElement[IWCC.AvatarElementCount];

        [field: S(9)] public int Living { get; set; }
        [field: S(9)] public int Kiling { get; set; }
        [field: S(9)] public int Nimble { get; set; }
        [field: S(9)] public int Defend { get; set; }
        [field: S(9)] public byte SkillPoint { get; set; }


        [field: S(17)] public List<IObjectAccessor<ICoreSkill>> LearnedCoreSkills { get; } = new();

        [field: S(18)]
        public IObjectAccessor<ICoreSkill>[] CoreSkills { get; } =
            new IObjectAccessor<ICoreSkill>[IWCC.CoreSkillCount];

        [field: S(19)] public List<IObjectAccessor<IFormSkill>> LearnedFormSkills { get; } = new();

        [field: S(20)]
        public IObjectAccessor<IFormSkill>[] FormSkills { get; } =
            new IObjectAccessor<IFormSkill>[IWCC.FormSkillCount];

        [field: S(21)] public List<IObjectAccessor<IPassiveSkill>> LearnedPassiveSkills { get; } = new();

        [field: S(22)]
        public IObjectAccessor<IPassiveSkill>[] PassiveSkills { get; } =
            new IObjectAccessor<IPassiveSkill>[IWCC.PassiveSkillCount];

        [field: S(23)] public List<IObjectAccessor<IItem>> Items { get; } = new();

        [field: S(16)] public IObjectAccessor<IEquipment> Equipment { get; set; } 

        // 认知
        [field: S(10)] public float Recognize { get; set; }

        // 稳定 
        [field: S(10)] public float Stability { get; set; }

        // 能力 
        [field: S(10)] public float Confident { get; set; }

        // 效益
        [field: S(10)] public float Efficient { get; set; }

        // 合群
        [field: S(11)] public float Gregarious { get; set; }

        // 利他
        [field: S(11)] public float Altruistic { get; set; }

        // 理性 
        [field: S(12)] public float Rationality { get; set; }

        // 远见
        [field: S(12)] public float Foresighted { get; set; }

        // 渊博
        [field: S(12)] public float Intelligent { get; set; }

        [field: S(13)] public float AreaView { get; set; }
        [field: S(13)] public float LifeView { get; set; }

        [field: S(13)] public float ValueView { get; set; }
        [field: S(13)] public float WorldView { get; set; }
        [field: S(25)] public List<IObjectAccessor<IRoleBuff>> RoleBuffs { get; } = new();
        [field: S(24)] public List<IObjectAccessor<IRoleProperty>> ExtraProperties { get; } = new();


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
                    Level = SkillPoint * 0x80000,
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
                role.FormSkill.Set(skill, (byte)(i / IWCC.FormSkillTypeCount),
                    (byte)(i % IWCC.FormSkillTypeCount));
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

            role.SetEquipment(Equipment?.Value);

            foreach (IObjectAccessor<IRoleProperty> extraProperty in ExtraProperties)
            {
                role.Properties.Install(extraProperty.Value);
            }

            foreach (IObjectAccessor<IRoleBuff> buff in RoleBuffs)
            {
                role.Buffs.Install(buff.Value);
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