using InfinityWorldChess.GlobalDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.SkillDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.AssetLoading;
using Secyud.Ugf.HexMap;
using Secyud.Ugf.Resource;
using UnityEngine;
using UnityEngine.Playables;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{
	public class PassiveSkillTemplate : ResourcedBase, IPassiveSkill
	{
		[R(3,true)] public string ShowDescription { get; set; }
		[R(4)] public byte Score { get; set; }
		[R(5)] public short Yin { get; set; }
		[R(6)] public short Yang { get; set; }
		[R(7)] public byte Living { get; set; }
		[R(8)] public byte Kiling { get; set; }
		[R(9)] public byte Nimble { get; set; }
		[R(10)] public byte Defend { get; set; }
		public string ShowName => Descriptor?.Name;
		public virtual string HideDescription => "没有特殊效果。";
		public IObjectAccessor<Sprite> ShowIcon { get; set; }
		public int SaveIndex { get; set; }
		public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }

		public virtual void Equip(Role role)
		{
		}

		public virtual void UnEquip(Role role)
		{
		}

		public virtual void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
		}

		protected override void SetDefaultValue()
		{
			ShowIcon =AtlasSpriteContainer.Create(IwcAb.Instance, Descriptor, 0);
			UnitPlay = PrefabContainer<HexUnitPlay>.Create(IwcAb.Instance, Descriptor,2);
		}
	}
}