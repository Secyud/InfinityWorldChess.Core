using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TreeViewComponents;
using UnityEngine;

namespace InfinityWorldChess.ActivityDomain
{
    public class ActivityGroup:ITreeNode,IShowable
    {
        [field:S]public string ShowName { get; set; }
        [field:S]public string ShowDescription { get;set; }
        [field:S]public IObjectAccessor<Sprite> ShowIcon { get; set;}
        [field:S]public List<Activity> Activities { get; } = new();
        public ActivityState State { get; set; }
        
        public bool Collapsed { get; set; }
        public IList<ITreeNode> SubNodes => Activities
            .Where(u=>u.State!=ActivityState.NotReceived)
            .Cast<ITreeNode>().ToList();
        public void SetNode(TreeNodeView node)
        {
            node.GetComponent<ShownCell>().BindShowable(this);
            node.GetComponent<ActivityStateViewer>().SetState(State);
        }

        /// <summary>
        /// Give mission rewards
        /// </summary>
        public virtual void FinishOperation()
        { 
			
        }
    }
}