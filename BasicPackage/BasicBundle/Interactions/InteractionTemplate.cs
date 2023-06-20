using System;
using System.Collections.Generic;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.Resource;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class InteractionTemplate : ResourcedBase, IInteractionUnit
    {
        [R(4)] public string Text { get; set; }
        [R(5)] public string Answer { get; set; }

        public IObjectAccessor<Sprite> Background { get; set; }
        public InteractionAction SelectAction { get; set; }

        public IList<Tuple<string, IInteractionUnit>> Selections { get; } =
            new List<Tuple<string, IInteractionUnit>>();

        public void OnStart()
        {
        }

        public void OnEnd()
        {
            SelectAction?.Invoke();
        }

        private InteractionAction GetAction(ResourceDescriptor descriptor, int id)
        {
            string actionName = descriptor.Get<string>(id);
            if (actionName.IsNullOrEmpty()) return null;
            string[] actParam = actionName.Split(',');
            ResourceDescriptor actionDescriptor = Og.InitializeManager.GetResource<InteractionAction>(actParam[0]);
            InteractionAction action = Og.ClassManager.Construct<InteractionAction>(actionDescriptor.TypeId);
            action.SetAction(actParam[1..]);
            return action;
        }

        protected override void SetDefaultValue()
        {
            Background = AtlasSpriteContainer.Create(
                IwcAb.Instance, Descriptor, 0);
            SelectAction = GetAction(Descriptor, 2);
            string[] names = Descriptor.Get<string>(3).Split(",");
            Selections.Clear();
            foreach (string n in names)
            {
                string name = n.Trim();
                if (name.IsNullOrEmpty())
                    continue;
                InteractionTemplate template = new InteractionTemplate().Init(name);
                Selections.Add(new Tuple<string, IInteractionUnit>(
                    template.Answer,
                    template
                ));
            }
        }
    }
}