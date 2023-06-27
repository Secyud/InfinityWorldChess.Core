using System.IO;
using UnityEngine;

namespace InfinityWorldChess
{
    public static class SharedConsts
    {
        public const int MaxBodyPartsCount = 4;
        public const int PassiveSkillCount = 3;
        public const int TotalCoreSkillCount = 0b11110;
        public const int CoreSkillLayerCount = 4;
        public const int CoreSkillCodeCount = 2;
        public const int CoreSkillCount = CoreSkillLayerCount * CoreSkillCodeCount;
        public const int FormSkillStateCount = 3;
        public const int FormSkillTypeCount = 3;
        public const int FormSkillCount = FormSkillStateCount * FormSkillTypeCount;
        public const int EquipmentPropertyCount = 4;


        public const int MaxChessResourceLevel = 5;
        public const int MaxChessResourceTypeCount = 1;
        public const int MaxChessSpecialTypeCount = 1;


        public const int MaxWorldResourceLevel = 5;
        public const int MaxWorldResourceTypeCount = 3;
        public const int MaxWorldSpecialTypeCount = 2;

        public const int SlotsCount = 8;
        public const int MaxExecutionValue = 32;
        public const int BattleTimeFactor = 256;

        public const int NatureCount = 9;

        public static readonly string SavePath = InitFolderPath(Application.dataPath, "SaveFiles");

        public static bool LoadGame { get; set; }= false;
        
        public static int SaveFolder { get; set; }
        
        public static string SaveFilePath(string fileName)
        {
            return Path.Combine(SavePath,SaveFolder.ToString(),fileName+".binary");
        }
        
        private static string InitFolderPath(params string[] seg)
        {
            string path = Path.Combine(seg);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}