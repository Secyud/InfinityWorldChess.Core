using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
    public class CustomDrag : Consumable, IHasFlavor
    {
        public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];

        public override void Save(IArchiveWriter writer)
        {
            this.SaveFlavors(writer);
            SaveActions(writer);
            SaveShown(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            this.LoadFlavors(reader);
            LoadActions(reader);
            LoadShown(reader);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
            transform.AddFlavorInfo(this);
            SetEffectContent(transform);
        }
    }
}