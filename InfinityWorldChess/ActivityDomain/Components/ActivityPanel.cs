using InfinityWorldChess.PlayerDomain;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityPanel : EditorBase<ActivityProperty>
    {
        [SerializeField] private LayoutGroupTrigger Content;
        [SerializeField] private LayoutGroupTrigger List;
        [SerializeField] private ActivityGroupLineViewer Prefab;

        public LayoutGroupTrigger ListContent => List;

        public void SelectActivity(IActivity activity)
        {
            activity.SetContent(Content.PrepareLayout());
        }

        protected override void InitData()
        {
            RectTransform rect = List.PrepareLayout();
            foreach (ActivityGroup group in Property)
            {
                ActivityGroupLineViewer instance = Prefab.Instantiate(rect);
                instance.Panel = this;
                instance.Bind(group);
            }
        }
        protected override void ClearUi()
        {
            Content.PrepareLayout();
            List.PrepareLayout();
        }
    }
}