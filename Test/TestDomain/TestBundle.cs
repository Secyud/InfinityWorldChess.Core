using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using UnityEngine;

namespace InfinityWorldChess.TestDomain
{
    public class TestBundle:IBundle
    {
        public string Description => "提供测试的初始工具以及武学。";
        public string Name => "测试工具包";
        public IObjectAccessor<Sprite> Icon => null;
        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public void OnGameCreation()
        {
            Role role = GameScope.Instance.Player.Role;
            
            role.Item.Add(new TestSkillPackageItem());
        }

        public void OnGameLoading()
        {
            
            
        }

        public void OnGameSaving()
        {
            
        }
    }
}