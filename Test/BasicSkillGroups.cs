// using InfinityWorldChess.SkillBundle;
// using InfinityWorldChess.SkillBundle.CoreSkill;
// using InfinityWorldChess.SkillDomain;
// using Secyud.Ugf.Resource;
//
// namespace InfinityWorldChess
// {
// 	public static class BasicSkillGroups
// 	{
// 		public static ICoreSkill[] ConstructBasic()
// 		{
// 			return new ICoreSkill[]
// 			{
// 				new NormalCoreBundle.AddPenetration().Init("刺"),
// 				new NormalCoreBundle.AddAttackFactor().Init("劈"),
// 				new SpecialCoreBundle.MultiAttackFactor1().Init("崩"),
// 				new CoreSkillTemplate().Init("扫"),
// 				new CoreSkillTemplate().Init("抓"),
// 				new SpecialCoreBundle.MultiAttackFactor0().Init("拿"),
// 				new NormalCoreBundle.MinusDefendFactor().Init("撩"),
// 				new CoreSkillTemplate().Init("擂"),
// 				new CoreSkillTemplate().Init("点"),
// 				new CoreSkillTemplate().Init("砸")
// 			};
// 		}
// 		
// 		public static ICoreSkill[] ConstructTianLongZhang()
// 		{
// 			return new ICoreSkill[]
// 			{
// 				new NormalCoreBundle.AddEnergy().Init("亢龙有悔"),
// 				new BasicCoreBundle.AddQianLongBuff().Init("潜龙勿用"),
// 				new CoreSkillTemplate().Init("含章可贞"),
// 				new SpecialCoreBundle.LowHealthAddAttack().Init("龙战于野"),
// 				new SpecialCoreBundle.RemoveDeBuff().Init("黄裳元吉"),
// 				new NormalCoreBundle.AddTargetTime().Init("飞龙在天"),
// 				new SpecialCoreBundle.DistanceAddDamage().Init("见龙在田"),
// 				new BasicCoreBundle.QianLongAddDamage().Init("群龙无首"),
// 				new SpecialCoreBundle.SkillLayerAddAttack().Init("无誉无咎"),
// 				new CoreSkillTemplate().Init("方大不习"),
// 				new SpecialCoreBundle.RepelOrAttackMore().Init("或跃在渊"),
// 				new NormalCoreBundle.DamageAddHealth().Init("坤利永贞"),
// 				new NormalCoreBundle.AddSlow().Init("坚冰履霜"),
// 			};
// 		}
// 	}
// }