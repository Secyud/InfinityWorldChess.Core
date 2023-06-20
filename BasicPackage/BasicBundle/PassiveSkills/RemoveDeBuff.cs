﻿using System;
using InfinityWorldChess.BasicBundle.BattleBuffs;
using InfinityWorldChess.BasicBundle.BattleBuffs.Recorders;
using InfinityWorldChess.BattleDomain;
using Secyud.Ugf.Resource;

namespace InfinityWorldChess.BasicBundle.PassiveSkills
{
    public class RemoveDeBuff : PassiveSkillTemplate, IOnBattleRoleInitialize
    {
        [R(256)] private byte DeBuffType { get; set; }

        [R(257)] private int Value { get; set; }

        private RoleBattleChess _chess;

        public override string HideDescription =>
            $"每回合移除{DeBuffType switch { 1 => "灼烧", 2 => "冰冻", 3 => "中毒", _ => "未知" }}状态({Value})。";


        public void OnBattleInitialize(RoleBattleChess chess)
        {
            _chess = chess;
        }

        public void Active()
        {
            switch (DeBuffType)
            {
                case 3:
                {
                    TimeRecorder recorder = _chess.Get<PoisonBuff>()?.TimeRecorder;
                    if (recorder is null) return;
                    recorder.TimeFinished -= Value;
                    if (recorder.TimeFinished <= 0)
                        _chess.UnInstall<PoisonBuff>();
                    return;
                }
                case 2:
                {
                    FrozenBuff recorder = _chess.Get<FrozenBuff>();
                    recorder.LayerCount -= Value;
                    if (recorder.LayerCount <= 0)
                        _chess.UnInstall<FrozenBuff>();
                    return;
                }
                case 1:
                {
                    FiringBuff recorder = _chess.Get<FiringBuff>();
                    recorder.LayerCount -= Value;
                    if (recorder.LayerCount <= 0)
                        _chess.UnInstall<FiringBuff>();
                    return;
                }
                default:
                    return;
            }
        }
    }
}