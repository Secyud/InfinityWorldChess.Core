using System;
using System.Collections.Generic;
using Secyud.Ugf.DataManager;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public abstract class InteractionAction
    {
        public abstract void Invoke();

        public static void Register(InitializeManager initializeManager)
        {
            Type type = typeof(InteractionAction);
            Dictionary<string, ResourceDescriptor> dict = initializeManager.GetOrAddDescriptors(type);

            void AddDescriptor<TAction>()
                where TAction : InteractionAction
            {
                Type t = typeof(TAction);
                string name = t.Name;
                Guid tid = TypeIdMapper.GetId(t);
                dict[name] = new ResourceDescriptor(name, tid, type);
            }

            AddDescriptor<AddPassiveSkill>();
            AddDescriptor<AddFormSkill>();
            AddDescriptor<AddItem>();
            AddDescriptor<AddCoreSkill>();
        }
    }
}