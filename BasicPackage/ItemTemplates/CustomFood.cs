#region

using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public sealed class CustomFood : Consumable, IHasFlavor, IHasMouthfeel
    {
        public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];
        public float[] MouthFeelLevel { get; } = new float[BasicConsts.MouthFeelCount];

        public override void Save(IArchiveWriter writer)
        {
            
            this.SaveMouthFeel(writer);
            this.SaveFlavors(writer);
            SaveActions(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            this.LoadMouthFeel(reader);
            this.LoadFlavors(reader);
            LoadActions(reader);
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddFlavorInfo(this);
            transform.AddMouthFeelInfo(this);
            SetEffectContent(transform);
        }
    }
}