using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.ItemDomain.FoodDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.ItemTemplates
{
    public class  Consumable : Item, IEdible, IEdibleInBattle, IAttachProperty
    {
        [field: S(64)] public List<IActionable<Role>> EffectsInWorld { get; } = new();
        [field: S(64)] public List<IActionable<BattleRole>> EffectsInBattle { get; } = new();
        [field: S(6)] public byte Living { get; set; }
        [field: S(6)] public byte Kiling { get; set; }
        [field: S(6)] public byte Nimble { get; set; }
        [field: S(6)] public byte Defend { get; set; }

        public void Eating(Role role)
        {
            foreach (IActionable<Role> buff in EffectsInWorld)
            {
                this.Attach(buff);
                buff.Invoke(role);
            }
        }

        public void EatingInBattle(BattleRole role)
        {
            foreach (IActionable<BattleRole> buff in EffectsInBattle)
            {
                this.Attach(buff);
                buff.Invoke(role);
            }
        }


        protected void SaveActions(IArchiveWriter writer)
        {
            writer.Write(EffectsInWorld.Count);
            foreach (IActionable<Role> actionable in EffectsInWorld)
            {
                writer.WriteObject(actionable);
            }

            writer.Write(EffectsInBattle.Count);
            foreach (IActionable<BattleRole> actionable in EffectsInBattle)
            {
                writer.WriteObject(actionable);
            }
        }

        protected void LoadActions(IArchiveReader reader)
        {
            EffectsInWorld.Clear();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                EffectsInWorld.Add(reader.ReadObject<IActionable<Role>>());
            }

            EffectsInBattle.Clear();
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                EffectsInBattle.Add(reader.ReadObject<IActionable<BattleRole>>());
            }
        }

        public override void SetContent(Transform transform)
        {
            base.SetContent(transform);
            SetEffectContent(transform);
        }

        protected void SetEffectContent(Transform transform)
        {
            transform.AddTitle2("非战斗中使用效果：");

            foreach (IActionable<Role> actionable in EffectsInWorld)
            {
                actionable.TrySetContent(transform);
            }

            transform.AddTitle2("战斗中使用效果：");

            foreach (IActionable<BattleRole> actionable in EffectsInBattle)
            {
                actionable.TrySetContent(transform);
            }
        }
    }
}