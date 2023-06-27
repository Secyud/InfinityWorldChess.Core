using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityResult : DataObject, ICanBeShown, IHasContent
    {
        public string ShowName => ObjectName;
        
        [field: S(ID = 0,DataType = DataType.Initialed)]
        public string ShowDescription { get; set; }
        
        [field: S(ID = 1,DataType = DataType.Initialed)]
        public IObjectAccessor<Sprite> ShowIcon { get; set; }

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }
    }
}