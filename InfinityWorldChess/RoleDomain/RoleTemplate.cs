using System.Collections.Generic;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.SkillDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;

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
                    Description = Description,
                    Avatar = Avatar
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

            RoleGameContext roleGameContext = GameScope.Instance.Role;
            roleGameContext[role.Id] = role;
            HexCell cell = GameScope.Instance.Map.Value.GetCell(PositionIndex);
            role.Position = cell as WorldCell;

            foreach (IObjectAccessor<ICoreSkill> coreSkill in LearnedCoreSkills)
            {
                role.CoreSkill.TryAddLearnedSkill(coreSkill.Value);
            }

            for (int i = 0; i < CoreSkills.Length; i++)
            {
                var coreSkillAccessor = CoreSkills[i];
                if (coreSkillAccessor is null) continue;

                var coreSkill = coreSkillAccessor.Value;
                role.CoreSkill.TryAddLearnedSkill(coreSkill);
                Role.CoreSkillProperty.GetCodeAndLayer(i, out byte layer, out byte code);
                role.CoreSkill.Set(coreSkill, layer, code);
            }

            foreach (IObjectAccessor<IFormSkill> formSkill in LearnedFormSkills)
            {
                role.FormSkill.TryAddLearnedSkill(formSkill.Value);
            }

            for (int i = 0; i < FormSkills.Length; i++)
            {
                var formSkillAccessor = FormSkills[i];
                if (formSkillAccessor is null) continue;

                var formSkill = formSkillAccessor.Value;
                role.FormSkill.TryAddLearnedSkill(formSkill);
                role.FormSkill.Set(formSkill, (byte)(i / SharedConsts.FormSkillTypeCount),
                    (byte)(i % SharedConsts.FormSkillTypeCount));
            }

            foreach (IObjectAccessor<IPassiveSkill> passiveSkill in LearnedPassiveSkills)
            {
                role.PassiveSkill.TryAddLearnedSkill(passiveSkill.Value);
            }

            for (int i = 0; i < PassiveSkills.Length; i++)
            {
                var passiveSkillAccessor = PassiveSkills[i];
                if (passiveSkillAccessor is null) continue;

                var passiveSkill = passiveSkillAccessor.Value;
                role.PassiveSkill.TryAddLearnedSkill(passiveSkill);
                role.PassiveSkill[i, role] = passiveSkill;
            }

            foreach (IObjectAccessor<IItem> item in Items)
            {
                role.Item.Add(item.Value);
            }

            for (int i = 0; i < Equipments.Length; i++)
            {
                var equipmentAccessor = Equipments[i];
                if (equipmentAccessor is null) continue;

                IEquipment equipment = equipmentAccessor.Value;
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
        }
    }
}