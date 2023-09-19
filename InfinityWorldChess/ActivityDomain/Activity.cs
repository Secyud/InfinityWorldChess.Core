#region

using System.Collections.Generic;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.TreeViewComponents;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ActivityDomain
{
	public class Activity : IShowable, IHasContent
	{
		[field:S]public string ShowName { get; set; }
		[field:S]public string ShowDescription { get; set;}
		[field:S]public IObjectAccessor<Sprite> ShowIcon { get; set;}
		public ActivityState State { get; set; }
		public IList<ITreeNode> SubNodes => null;
		
		public virtual void SetContent(Transform transform)
		{
			
		}

		
		/// <summary>
		/// add event to trigger finish state
		/// </summary>
		public virtual void InitActivity()
		{
			
		}

		/// <summary>
		/// Give mission rewards
		/// </summary>
		protected virtual void FinishOperation()
		{ 
			
		}
	}
}