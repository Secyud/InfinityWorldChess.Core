using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    [ID("9AF58D20-1D76-14A8-3283-9DCF21B4E749")]
    public class RoleAddProperty : IEquippable<Role>,IHasContent
    {
        [field: S] public float LivingFactor { get; set; }
        [field: S] public float KilingFactor { get; set; }
        [field: S] public float NimbleFactor { get; set; }
        [field: S] public float DefendFactor { get; set; }

        public PassiveSkill Skill { get; set; }
        private readonly int[] _properties = new int[SharedConsts.MaxBodyPartsCount];

        private Role.BodyPartProperty Body => Skill.Role.BodyPart;
        
        public float this[int i] => i switch
        {
            0 => LivingFactor,
            1 => KilingFactor,
            2 => NimbleFactor,
            3 => DefendFactor,
            _ => 0
        };
        public void SetContent(Transform transform)
        {
            transform.AddParagraph("增加四维属性{}。");
        }

        public void Install(Role target)
        {
            for (int i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
            {
                _properties[i] = (int)(Skill[i] * this[i]);
                Body[(BodyType)i].MaxValue += _properties[i];
            }
        }

        public void UnInstall(Role target)
        {
            for (int i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
            {
                Body[(BodyType)i].MaxValue -= _properties[i];
            }
        }
    }
}