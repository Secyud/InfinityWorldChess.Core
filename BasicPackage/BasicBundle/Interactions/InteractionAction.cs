using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public abstract class InteractionAction
    {
        public abstract void SetAction([NotNull] params string[] message);

        public abstract void Invoke();

        public static void Register(InitializeManager initializeManager)
        {
            Type type = typeof(InteractionAction);
            Dictionary<string, ResourceDescriptor> dict = initializeManager.GetOrAddTemplate(type);

            void AddDescriptor<TAction>()
                where TAction:InteractionAction
            {
                Type t = typeof(TAction);
                string name = t.Name;
                Guid tid = t.GetId();
                dict[name] = new ResourceDescriptor(name,tid, type);
            }

            AddDescriptor<AddPassiveSkill>();
            AddDescriptor<AddFormSkill>();
            AddDescriptor<AddItem>();
            AddDescriptor<AddCoreSkill>();
        }
    }
}