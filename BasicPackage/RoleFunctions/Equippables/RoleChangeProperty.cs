using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.RoleFunctions
{
    [ID("9AF58D20-1D76-14A8-3283-9DCF21B4E749")]
    public class RoleChangeProperty : IEquippable<Role>, IHasContent, IPropertyAttached
    {
        [field: S] public float LivingFactor { get; set; }
        [field: S] public float KilingFactor { get; set; }
        [field: S] public float NimbleFactor { get; set; }
        [field: S] public float DefendFactor { get; set; }
        public IAttachProperty Property { get; set; }

        private readonly int[] _propertyChanged = new int[SharedConsts.MaxBodyPartsCount];

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
            transform.AddParagraph(
                $"改变四维属性, {LivingFactor:P0}[生], {KilingFactor:P0}[杀], {NimbleFactor:P0}[灵], {DefendFactor:P0}[御]。");
        }

        public void Install(Role target)
        {
            for (int i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
            {
                _propertyChanged[i] = (int)(Property.Get(i) * this[i]);
                target.BodyPart[(BodyType)i].MaxValue += _propertyChanged[i];
            }
        }

        public void UnInstall(Role target)
        {
            for (int i = 0; i < SharedConsts.MaxBodyPartsCount; i++)
            {
                target.BodyPart[(BodyType)i].MaxValue -= _propertyChanged[i];
            }
        }
    }
}