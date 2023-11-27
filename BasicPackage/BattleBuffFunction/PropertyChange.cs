using System;
using InfinityWorldChess.BattleBuffDomain;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.FunctionDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleBuffFunction
{
    public class PropertyChange : IBuffEffect, IHasContent
    {
        [field: S] public int Value { get; set; }
        [field: S] public byte Type { get; set; }

        public BattleRoleBuff BelongBuff { get; set; }

        private BattleProperty Property { get; set; }
        private BodyType BodyType => (BodyType)Type;

        public void InstallFrom(BattleRole target)
        {
            Property = target.Properties.GetOrCreate<BattleProperty>();
            Property[BodyType] += Value;
        }

        public void UnInstallFrom(BattleRole target)
        {
            Property[BodyType] -= Value;
        }


        public void Overlay(IOverlayable<BattleRole> finishBuff)
        {
            if (finishBuff is not BattleRoleBuff
                {
                    Effect: PropertyChange effect
                })
                return;

            Property[BodyType] += Value - effect.Value;

            effect.Value = Value;
        }

        public void SetContent(Transform transform)
        {
            string bodyType = BodyType switch
            {
                BodyType.Living => "精气",
                BodyType.Kiling => "杀伐",
                BodyType.Nimble => "灵敏",
                BodyType.Defend => "防御",
                _               => throw new ArgumentOutOfRangeException()
            };

            if (Value > 0)
            {
                transform.AddParagraph($"增加{Value:N0}点{bodyType}。");
            }
            else if (Value < 0)
            {
                transform.AddParagraph($"减少{-Value:N0}点{bodyType}。");
            }
        }
    }
}