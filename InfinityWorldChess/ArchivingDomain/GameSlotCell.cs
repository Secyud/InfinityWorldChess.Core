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
        [SerializeField] private GameLoadPanel LoadPanel;
        [SerializeField] private AvatarEditor Avatar;
        [SerializeField] private EditorEvent<string> NameText;
        [SerializeField] private EditorEvent<bool> SlotEnabled;

        private string _slotMessagePath;
        private int _slotIndex;

        private void Awake()
        {
            _slotIndex = transform.GetSiblingIndex();
        }

        private void Start()
        {
            _slotMessagePath = Path.Combine(SharedConsts.SavePath,
                _slotIndex.ToString(), "slot.binary");
            if (File.Exists(_slotMessagePath))
            {
                using FileStream fileStream = File.OpenRead(_slotMessagePath);
                DefaultArchiveReader reader = new(fileStream);
                Role.BasicProperty basic = new();
                basic.Load(reader);
                Avatar.OnInitialize(basic);
                NameText.Invoke(basic.Name);
                SlotEnabled.Invoke(true);
            }
            else
            {
                Avatar.OnInitialize(null);
                NameText.Invoke("空存档");
                SlotEnabled.Invoke(false);
            }
        }


        public void OnSlotLoad()
        {
            SharedConsts.SaveFolder = _slotIndex;
            U.M.DestroyScope<MainMenuScope>();
            ArchivingScope.Instance.CloseGameLoadPanel();
            if (File.Exists(_slotMessagePath))
            {
                U.Factory.InitializeGame();
                IwcAssets.Instance.LoadingPanelInk.Instantiate();
            }
            else
            {
                U.M.CreateScope<GameCreatorScope>();
            }
        }

        public void OnSlotDelete()
        {
            "确定要删除存档吗?".CreateEnsureFloatingOnCenter(DeleteSlot);
        }

        private void DeleteSlot()
        {
            Directory.Delete(Path.Combine(SharedConsts.SavePath,
                _slotIndex.ToString()), true);
            Avatar.OnInitialize(null);
            NameText.Invoke("空存档");
            SlotEnabled.Invoke(false);
        }
    }
}