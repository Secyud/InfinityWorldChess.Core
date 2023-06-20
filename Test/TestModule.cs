#region

using InfinityWorldChess.ActivityDomain;
using InfinityWorldChess.BasicBundle.CoreSkills;
using InfinityWorldChess.BasicBundle.PassiveSkills;
using InfinityWorldChess.BundleDomain;
using InfinityWorldChess.ItemDomain.BookDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using InfinityWorldChess.ManufacturingDomain;
using InfinityWorldChess.PlayerDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.Modularity;
using Secyud.Ugf.Resource;
using UnityEngine;

#endregion

namespace InfinityWorldChess
{
	[DependsOn(typeof(BasicPackageModule))]
	public class TestModule : IUgfModule, IPostConfigure, IOnPostInitialization, IOnGameArchiving
	{

		public void ConfigureGame(ConfigurationContext context)
		{
			context.Manager.AddAssembly(typeof(TestModule).Assembly);
		}

		public void PostConfigureGame(ConfigurationContext context)
		{
			IBundleGlobalService abgc = context.Get<IBundleGlobalService>();
			abgc.Bundles.Register(new TestBundle());
			context.Get<StoneEquipmentManufacturingContext>().DefaultProcesses.Register(new TestProcess());
		}

		public void OnGamePostInitialization()
		{
			PlayerGameContext pgc = GameScope.PlayerGameContext;
			TestFormSkill formSkill = new TestFormSkill();
			pgc.Role.FormSkill.LearnedSkills.Add(formSkill);
			pgc.Role.FormSkill.Set(formSkill, 4);
			// ICoreSkill[] skills = BasicSkillGroups.ConstructBasic();
			// foreach (ICoreSkill skill in skills)
			// {
			// 	pgc.Role.CoreSkill.LearnedSkills.Add(skill);
			// 	pgc.Role.CoreSkill.Set(skill, skill.MaxLayer, skill.FullCode);
			// }
			无敌一拳 wd = new 无敌一拳();
			pgc.Role.CoreSkill.LearnedSkills.Add(wd);
			pgc.Role.CoreSkill.Set(wd, wd.MaxLayer, wd.FullCode);

			PassiveSkillTemplate sk = new()
			{
				Living = 255
			};
			pgc.Role.PassiveSkill.LearnedSkills.Add(sk);
			pgc.Role.SetPassiveSkill(sk,0);
			Role.ItemProperty item = pgc.Role.Item;
			item.Add(new TestStoneEquipmentRaw());
			Equipment e = new Equipment
			{
				Name = "测试装备",
				EquipCode = 0b1111
			};

			const int b = 200;
			e.Property[0] = Random.Range(0, b);
			for (int i = 1; i < SharedConsts.EquipmentPropertyCount; i++)
			{
				e.Property[i] = e.Property[i - 1] + Random.Range(0, b);
			}

			item.Add(e);
			item.Add(
				new CoreSkillBook
				{
					Skill = new CoreSkillTemplate(),
				}
			);
		}

		public void OnGameLoading(LoadingContext context)
		{
		}

		public void OnGameSaving(SavingContext context)
		{
		}

		public void OnGameCreation()
		{
			PlayerGameContext p = GameScope.PlayerGameContext;
			Og.Get<GameScope,ManufacturingGameContext>().AddButtonToChecker(
				new ManufacturingButtonRegistration()
				{
					Type = 2
				}, p.Role.Position
			);
		}
	}
}