#region

using System;
using System.Collections.Generic;
using System.Linq;
using InfinityWorldChess.GlobalDomain;

#endregion

namespace InfinityWorldChess.BuffDomain
{
	public class ActionableContainer<TTarget>
	{
		private readonly Dictionary<Type, IActionable<TTarget>> _actions = new();


		public TAction Get<TAction>() where TAction : class, IActionable<TTarget>
		{
			_actions.TryGetValue(typeof(TAction), out IActionable<TTarget> ret);
			if (ret is null)
			{
				TAction action = typeof(TAction).GetConstructor(Type.EmptyTypes)!
					.Invoke(Array.Empty<object>()) as TAction;
				_actions[typeof(TAction)] = action;
				return action;
			}

			return ret as TAction;
		}

		public void Add<TAction>(TAction action) where TAction : class, IActionable<TTarget>
		{
			if (action is not null)
				_actions[typeof(TAction)] = action;
		}

		public void Remove<TAction>() where TAction : class, IActionable<TTarget>
		{
			_actions.Remove(typeof(TAction));
		}

		public void Remove<TAction>(TAction action) where TAction : class, IActionable<TTarget>
		{
			if (_actions.TryGetValue(
					typeof(TAction),
					out IActionable<TTarget> ac
				) &&
				ac == action)
				_actions.Remove(typeof(TAction));
		}

		public void Clear()
		{
			_actions.Clear();
		}

		public void On(TTarget target)
		{
			foreach (IActionable<TTarget> action in
				_actions.Values.OrderBy(u => u.Priority))
				action.Active(target);
		}
	}
}