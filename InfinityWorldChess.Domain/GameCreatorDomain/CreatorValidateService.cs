using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ValidateComponents;

namespace InfinityWorldChess.GameCreatorDomain
{
    [Registry(DependScope=typeof(GameCreatorScope))]
    public class CreatorValidateService:ValidateService<CreatorValidateService,CreatorValidateItem>
    {
        
    }
}