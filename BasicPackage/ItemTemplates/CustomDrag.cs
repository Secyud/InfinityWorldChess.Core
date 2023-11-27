using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
    public class CustomDrag : Consumable
    {
        public float SpicyLevel { get; set; }
        public float SweetLevel { get; set; }
        public float SourLevel { get; set; }
        public float BitterLevel { get; set; }
        public float SaltyLevel { get; set; }
        
        public override void Save(IArchiveWriter writer)
        {
            writer.Write(SpicyLevel);
            writer.Write(SweetLevel);
            writer.Write(SourLevel);
            writer.Write(BitterLevel);
            writer.Write(SaltyLevel);
            SaveActions(writer);
            SaveShown(writer);
            this.SaveProperty(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            SpicyLevel  = reader.ReadSingle(); 
            SweetLevel  = reader.ReadSingle(); 
            SourLevel   = reader.ReadSingle();
            BitterLevel = reader.ReadSingle();
            SaltyLevel  = reader.ReadSingle();
            LoadActions(reader);
            LoadShown(reader);
            this.LoadProperty(reader);
        }

        public override void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
            
            transform.AddParagraph(
                " 辛: " + SpicyLevel +
                " 甘: " + SweetLevel + 
                " 酸: " + SourLevel +
                " 苦: " + BitterLevel + 
                " 咸: " + SaltyLevel
            );
            SetEffectContent(transform);
        }
    }
}