using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.BasicComponents;
using System.IO;
using Secyud.Ugf.Modularity;
using UnityEngine;

namespace InfinityWorldChess.GlobalDomain
{
    public class GameSlotCell : MonoBehaviour
    {
        [SerializeField] private SText Name;
        [SerializeField] private RectTransform Content;
        [SerializeField] private GameObject DeleteSlotButton;
        private int _index;
        private ISlot _slot;
        private string _path;
        private IArchivingContext _archivingContext;
        private IArchivingContext ArchivingContext => 
            _archivingContext ??= Og.DefaultProvider.Get<IArchivingContext>();

        public void OnClick()
        {
            ArchivingContext.CurrentSlot = ArchivingContext.Slots[_index];
            Og.ScopeFactory.DestroyScope<MainMenuScope>();
            if (ArchivingContext.CurrentSlotExist)
            {
                UgfApplicationFactory<StartupModule>.GameLoad();
                IwcAb.Instance.LoadingPanelInk.Instantiate();
            }
            else
                Og.ScopeFactory.CreateScope<CreatorScope>();
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
            _path = Path.Combine(Og.ArchivingPath, _slot.Id.ToString());
            DeleteSlotButton.SetActive(Directory.Exists(_path));
        }
    }
}