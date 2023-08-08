using System;
using Secyud.Ugf.ValidateComponents;

namespace InfinityWorldChess.GameCreatorDomain
{
    public class CreatorValidateItem : ValidateActionItem<CreatorValidateService,CreatorValidateItem>
    {
        public CreatorValidateItem(string name, Func<bool> action) : base(name, action)
        {
        }
    }
}