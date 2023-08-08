using System.IO;
using InfinityWorldChess.GameCreatorDomain;
using InfinityWorldChess.MainMenuDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.EditorComponents;
using UnityEngine;

namespace InfinityWorldChess.ArchivingDomain
{
    public class GameSlotCell : MonoBehaviour
    {
        [SerializeField] private int SlotIndex;
        [SerializeField] private GameLoadPanel LoadPanel;
        [SerializeField] private AvatarEditor Avatar;
        [SerializeField] private EditorEvent<string> NameText;
        [SerializeField] private EditorEvent<bool> SlotEnabled;

        private string _slotMessagePath;
        
        private void Start()
        {
            _slotMessagePath = Path.Combine(SharedConsts.SavePath,
                SlotIndex.ToString(), "slot.binary");
            if (File.Exists(_slotMessagePath))
            {
                using FileStream fileStream = File.OpenRead(_slotMessagePath);
                DefaultArchiveReader reader = new(fileStream);
                Role.BasicProperty basic = new();
                basic.Load(reader);
                Avatar.Bind(basic);
                NameText.Invoke(basic.Name);
                SlotEnabled.Invoke(true);
            }
            else
            {
                Avatar.Bind(null);
                NameText.Invoke("空存档");
                SlotEnabled.Invoke(false);
            }
        }


        public void OnSlotLoad()
        {
            SharedConsts.SaveFolder = SlotIndex;
            U.Factory.Application.DependencyManager.DestroyScope<MainMenuScope>();
            if (File.Exists(_slotMessagePath))
            {
                U.Factory.InitializeGame();
                IwcAb.Instance.LoadingPanelInk.Instantiate();
            }
            else
            {
                U.Factory.Application.DependencyManager.CreateScope<GameCreatorScope>();
            }
        }

        public void OnSlotDelete()
        {
            "确定要删除存档吗?".CreateEnsureFloatingOnCenter(DeleteSlot);
        }

        private void DeleteSlot()
        {
            Directory.Delete(Path.Combine(SharedConsts.SavePath,
                SlotIndex.ToString()), true);
            Avatar.Bind(null);
            NameText.Invoke("空存档");
            SlotEnabled.Invoke(false);
        }
    }
}