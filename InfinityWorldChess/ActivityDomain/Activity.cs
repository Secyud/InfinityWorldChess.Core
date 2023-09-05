#region

using System.Collections.Generic;
using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TreeViewComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ActivityDomain
{
	public class Activity : IShowable, IHasContent,IHasSaveIndex,ITreeNode
	{
		[field:S]public string ShowName { get; set; }
		[field:S]public string ShowDescription { get; set;}
		[field:S]public IObjectAccessor<Sprite> ShowIcon { get; set;}
		
		public ActivityState State { get; set; }
		public ActivityGroup Group { get; set; }
		public int SaveIndex { get; set; }
		public bool Collapsed { get; set; }
		public IList<ITreeNode> SubNodes => null;
		
		public virtual void SetContent(Transform transform)
		{
			
		}
		public void SetNode(TreeNodeView node)
		{
			node.GetComponent<ShownCell>().BindShowable(this);
			node.GetComponent<ActivityStateViewer>().SetState(State);
		}

		
		/// <summary>
		/// add event to trigger finish state
		/// </summary>
		public virtual void InitActivity()
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void FinishActivity(bool failed)
		{
			if (failed)
			{
				State = ActivityState.Failed;
				Group.State = ActivityState.Failed;
			}
			else
			{
				State = ActivityState.Finished;
				FinishOperation();
				
				int index = Group.Activities.IndexOf(this);
				if (index < 0 || index >= Group.Activities.Count - 1)
				{
					Group.State = ActivityState.Finished;
					Group.FinishOperation();
				}
				else
				{
					Activity nextActivity = Group.Activities[index + 1];
					nextActivity.State = ActivityState.Received;
					nextActivity.InitActivity();
				}
			}
		}

		/// <summary>
		/// Give mission rewards
		/// </summary>
		protected virtual void FinishOperation()
		{ 
			
		}
	}
}