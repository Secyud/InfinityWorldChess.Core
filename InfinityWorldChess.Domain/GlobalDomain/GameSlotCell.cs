using InfinityWorldChess.Ugf;
using Secyud.Ugf.BasicComponents;
using System.IO;
using InfinityWorldChess.ArchivingDomain;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
{
    public class GameSlotCell : MonoBehaviour
    {
        [SerializeField] private SText Name;
        [SerializeField] private RectTransform Content;
        [SerializeField] private GameObject DeleteSlotButton;
        private int _index;
        private ArchivingSlot _slot;
        private string _path;
        private ArchivingContext _archivingContext;

        private ArchivingContext ArchivingContext =>
            _archivingContext ??= U.Get<ArchivingContext>();

        public void OnClick()
        {
            ArchivingContext.CurrentSlot = ArchivingContext.Slots[_index];
            U.Factory.Application.DependencyManager.DestroyScope<MainMenuScope>();
            if (ArchivingContext.CurrentSlot.Exist)
            {
                U.Factory.GameInitialize();
                IwcAb.Instance.LoadingPanelInk.Instantiate();
            }
            else
            {
                U.Factory.Application.DependencyManager.CreateScope<CreatorScope>();
            }
        }

        public void OnDeleteSlotClick()
        {
            "确定要删除存档吗?".CreateEnsureFloatingOnCenter(DeleteSlot);
        }

        private void DeleteSlot()
        {
            Directory.Delete(_path, true);
            OnInitialize(_index);
        }


        public void OnInitialize(int index)
        {
            _index = index;
            _slot = ArchivingContext.Slots[_index];
            _slot.PrepareSlotLoading();
            Name.text = _slot.Name;
            _slot.SetContent(Content);
            _path = Path.Combine(SharedConsts.SavePath, _slot.Id.ToString());
            DeleteSlotButton.SetActive(Directory.Exists(_path));
        }
    }
}