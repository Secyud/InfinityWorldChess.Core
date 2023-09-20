using Secyud.Ugf.EditorComponents;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityLineViewer:EditorBase<IActivity>
    {
        [SerializeField] private UnityEvent<string> Name;
        [SerializeField] private UnityEvent<ActivityState> State;

        public ActivityGroupLineViewer  Group { get; set; }
        
        protected override void InitData()
        {
            Name.Invoke(Property.ShowName);
            State.Invoke(Property.State);
        }

        public void OnClick()
        {
            Group.Panel.SelectActivity(Property);
        }
    }
}