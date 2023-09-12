using System.Collections.Generic;
using Secyud.Ugf.AssetComponents;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.ObserverComponents;

namespace InfinityWorldChess.BattleDomain.BattleRoleDomain
{
    [Registry(DependScope = typeof(BattleScope))]
    public class RoleObservedService: ObservedService
    {
        public  ObservedService State { get; } = new();
        
        private readonly MonoContainer<BattlePlayerController>  _playerController = 
            MonoContainer<BattlePlayerController>.Create<IwcAssets>();
        
        private BattleRole _role;

        public BattleRole Role
        {
            get => _role;
            internal set
            {
                if (_role == value)
                    return;
                _role = value;

                if (_role.PlayerControl)
                {
                    _playerController.Create();
                }
                else
                {
                    _playerController.Destroy();
                }
                
                Refresh();
                RefreshState();
            }
        }

        public void RefreshState()
        {
            State.Refresh();
        }
    }
}