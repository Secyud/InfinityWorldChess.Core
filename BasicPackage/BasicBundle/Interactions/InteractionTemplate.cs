using System;
using System.Collections.Generic;
using System.Ugf;
using InfinityWorldChess.InteractionDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BasicBundle.Interactions
{
    public class InteractionTemplate : DataObject, IInteractionUnit
    {
        private List<Tuple<string, IInteractionUnit>> _selections;
        [field: S(ID = 0)] public string Text { get; set; }
        [field: S(ID = 1)] public string Answer { get; set; }
        [field: S(ID = 2)] public IObjectAccessor<Sprite> Background { get; set; }
        [field: S(ID = 3)] public InteractionAction SelectAction { get; set; }

        [field: S(ID = 4, Style = EditStyle.FlagOrMemo)]
        public string SelectionStr { get; set; }

        public IList<Tuple<string, IInteractionUnit>> Selections
        {
            get
            {
                if (_selections is null)
                {
                    _selections = new List<Tuple<string, IInteractionUnit>>();
                    string[] names = SelectionStr.Split(",");
                    foreach (string n in names)
                    {
                        string name = n.Trim();
                        if (name.IsNullOrEmpty())
                            continue;
                        InteractionTemplate template = Create<InteractionTemplate>(name);
                        _selections.Add(new Tuple<string, IInteractionUnit>(
                            template.Answer, template));
                    }
                }

                return _selections;
            }
        }

        public void OnStart()
        {
        }

        public void OnEnd()
        {
            SelectAction?.Invoke();
        }
    }
}