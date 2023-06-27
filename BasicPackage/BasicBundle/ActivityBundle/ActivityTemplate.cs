using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.ActivityBundle
{
    public class ActivityTemplate : DataObject, IActivity
    {
        public string ShowName => ObjectName;

        [field: S(ID = 0, DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }

        [field: S(ID = 1, DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        public IActivityGroup Group { get; set; }
        public ActivityState State { get; set; }
        public int SaveIndex { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnReceive()
        {
        }
    }
}