using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.HexMap;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

namespace InfinityWorldChess.SkillDomain
{
	public class CustomSkill : IActiveSkill, IArchivableShown,IArchivable
	{
		public string ShowName => Name;


		public string ShowDescription => Description;

		public IObjectAccessor<Sprite> ShowIcon => Icon;

		public SkillTargetType TargetType { get; set; }
		public bool Damage { get; set; }

		public byte Score { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
		public IObjectAccessor<HexUnitPlay> UnitPlay { get; set; }
		public IObjectAccessor<Sprite> Icon { get; set; }

		public ISkillCastCondition Condition { get; set; }

		public ISkillCastPosition Position { get; set; }

		public ISkillCastResult Result { get; set; }

		public List<ISkillCastEffect> Effects { get; } = new();

		public void SetContent(Transform transform)
		{
			transform.AddSimpleShown(this);
			Condition.SetContent(transform);
			Position.SetContent(transform);
			Result.SetContent(transform);
			foreach (ISkillCastEffect effect in Effects)
				effect.SetContent(transform);
		}

		public int SaveIndex { get; set; }

		public void Release()
		{
			foreach (ISkillCastEffect effect in Effects)
				effect.Release();
			{
				if (Icon is IReleasable releasable)
					releasable.Release();
			}
		}


		public string CheckCastCondition(RoleBattleChess chess)
		{
			return Condition.CheckCastCondition(chess);
		}

		public ISkillRange GetCastPositionRange(IBattleChess battleChess)
		{
			return Position.GetCastPositionRange(battleChess);
		}

		public ISkillRange GetCastResultRange(IBattleChess battleChess, HexCell castPosition)
		{
			return Result.GetCastResultRange(battleChess, castPosition);
		}

		public void Cast(IBattleChess battleChess, HexCell releasePosition)
		{
			foreach (ISkillCastEffect effect in Effects)
				effect.Cast(battleChess, releasePosition);
		}

		public void Save(BinaryWriter writer)
		{
			this.SaveShown(writer);
			
			writer.WriteArchiving(Condition);
			writer.WriteArchiving(Position);
			writer.WriteArchiving(Result);
			writer.Write(Effects.Count);
			writer.WriteArchiving(Effects);
		}

		public void Load(BinaryReader reader)
		{
			throw new System.NotImplementedException();
		}
	}
}