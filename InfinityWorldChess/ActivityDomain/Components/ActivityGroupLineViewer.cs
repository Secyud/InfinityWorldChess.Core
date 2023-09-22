using System;
using System.Linq;
using Secyud.Ugf.EditorComponents;
using Secyud.Ugf.LayoutComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroupLineViewer:EditorBase<ActivityGroup>
    {
        [SerializeField] private UnityEvent<string> Name;
        [SerializeField] private UnityEvent<ActivityState> State;
        [SerializeField] private LayoutGroupTrigger Content;
        [SerializeField] private ActivityLineViewer Prefab;

        public ActivityPanel  Panel { get; set; }

        public bool Collapsed
        {
            get => Property?.Collapsed == true;
            set
            {
                if (Property is not null)
                {
                    Property.Collapsed = value;
                    RectTransform content = Content.PrepareLayout();
                    Panel.ListContent.enabled = true;
                    Panel.ListContent.Record = 4;
                    if (value)
                    {
                        foreach (IActivity activity in Property.Activities
                                     .Where(activity => activity.State != ActivityState.NotReceived))
                        {
                            ActivityLineViewer instance = Prefab.Instantiate(content);
                            instance.Group = this;
                            instance.Bind(activity);
                        }
                    }
                }
            }
        }
        
        protected override void InitData()
        {
            Name.Invoke(Property.ShowName);
            State.Invoke(Property.State);
            Collapsed = Property.Collapsed;
        }

        protected override void ClearUi()
        {
            Content.PrepareLayout();
        }

        public void OnClick()
        {
            Collapsed = !Collapsed;
        }
    }
}