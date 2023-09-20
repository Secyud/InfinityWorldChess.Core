#region

using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ActivityDomain
{
	public class Activity : IActivity
	{
		[field:S]public string ShowName { get; set; }
		[field:S]public string ShowDescription { get; set;}
		[field:S]public IObjectAccessor<Sprite> ShowIcon { get; set;}
		
		public ActivityState State { get; set; }
		
		public virtual void SetActivity(ActivityGroup group)
		{
			
		}

		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}
	}
}