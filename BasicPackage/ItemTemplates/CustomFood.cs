#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemTemplates
{
    public sealed class CustomFood : Consumable
    {
        public float ColorLevel { get; set; }
        public float SmellLevel { get; set; }
        public float TasteLevel { get; set; }
        public override void Save(IArchiveWriter writer)
        {
            writer.Write(ColorLevel);
            writer.Write(SmellLevel);
            writer.Write(TasteLevel);
            SaveActions(writer);
            SaveShown(writer);
            this.SaveProperty(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            ColorLevel = reader.ReadSingle();
            SmellLevel = reader.ReadSingle();
            TasteLevel = reader.ReadSingle();
            LoadActions(reader);
            LoadShown(reader);
            this.LoadProperty(reader);
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            transform.AddParagraph(
                "色: " + ColorLevel +
                " 香: " + SmellLevel +
                " 味: " + TasteLevel 
            );
            SetEffectContent(transform);
        }
    }
}