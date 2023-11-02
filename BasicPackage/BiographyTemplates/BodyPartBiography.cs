#region

using InfinityWorldChess.BiographyDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.BiographyTemplates
{
    public sealed class BodyPartBiography : IBiography
    {
        [field: S] public int Value { get; set; }
        [field: S] public byte BodyType { get; set; }
        [field: S] public string Name { get; set; }
        [field: S] public string Description { get; set; }
        [field: S] public IObjectAccessor<Sprite> Icon { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnGameCreation(Role role)
        {
            role.BodyPart[(BodyType)BodyType].MaxValue += Value;
        }
    }
}