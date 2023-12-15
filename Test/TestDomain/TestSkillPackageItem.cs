using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.TestDomain
{
    public class TestSkillPackageItem : IItem, IEdible
    {
        public string Description => "测试武学包，可以获得基础武学。";
        public string Name => "测试武学包";
        public IObjectAccessor<Sprite> Icon => null;

        public void SetContent(Transform transform)
        {
            transform.AddSimpleShown(this);
        }

        public int SaveIndex { get; set; }
        public int Score => 0;

        public void Eating(Role role)
        {
            foreach (CoreSkill skill in GetTestCoreSkills())
            {
                role.CoreSkill.TryAddLearnedSkill(skill) ;
            }
            foreach (FormSkill skill in GetTestFormSkills())
            {
                role.FormSkill.TryAddLearnedSkill(skill);
            }
            foreach (PassiveSkill skill in GetTestPassiveSkills())
            {
                role.PassiveSkill.TryAddLearnedSkill(skill);
            }

            role.Item.Remove(this,1);
        }

        public CoreSkill[] GetTestCoreSkills()
        {
            return new[]
            {
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_撩"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_点"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_拿"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_擂"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_劈"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_砸"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_扫"),
                U.Tm.ReadObjectFromResource<CoreSkill>("基础招式_抓"),
            };
        }
        public FormSkill[] GetTestFormSkills()
        {
            return new[]
            {
                U.Tm.ReadObjectFromResource<FormSkill>("基本轻功_草上飞")
            };
        }
        public PassiveSkill[] GetTestPassiveSkills()
        {
            return new[]
            {
                U.Tm.ReadObjectFromResource<PassiveSkill>("吐纳术"),
            };
        }

        public string ResourceId { get; set; } = "测试资源";
    }
}