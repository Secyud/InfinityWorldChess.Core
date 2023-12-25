using System.Collections.Generic;
using System.Runtime.InteropServices;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using UnityEngine;

namespace InfinityWorldChess.BattleTemplates
{
    [Guid("7ADE420F-FB5E-F146-BD9F-3C0EBB1845F6")]
    public class Battle : IBattleDescriptor, IObjectAccessor<IBattleDescriptor>
    {
        [field: S(0)] public string ResourceId { get; set; }
        [field: S(2)] public int SizeX { get; set; }
        [field: S(2)] public int SizeZ { get; set; }
        [field: S(16)] public IBattleVictoryCondition VictoryCondition { get; set; }
        [field: S(1)] public string Description { get; set; }
        [field: S(0)] public string Name { get; set; }
        [field: S(15)] public IObjectAccessor<Sprite> Icon { get; set; }
        [field: S(18)] public List<BattleCampSetting> BattleCampSettings { get; } = new();
        public virtual WorldCell Cell => GameScope.Instance.Player.Role.Position;

        public virtual void OnBattleFinished()
        {
        }

        public virtual void OnBattleCreated()
        {
            foreach (BattleCampSetting setting in BattleCampSettings)
            {
                BattleCamp camp = new()
                {
                    Name = setting.Name,
                    Index = setting.Index,
                    Color = new Color(setting.R, setting.G, setting.B)
                };

                foreach (RoleWithPosition roleWithPosition in setting.CampRoles)
                {
                    Role role = roleWithPosition.RoleAccessor?.Value;
                    if (role is not null)
                    {
                        AddBattleUnit(role, roleWithPosition.PositionX,roleWithPosition.PositionZ, camp);
                    }
                }
            }

            VictoryCondition.SetCondition();
        }

        public bool Victory => VictoryCondition.Victory;
        public bool Defeated => VictoryCondition.Defeated;

        private static void AddBattleUnit(Role role, int x, int z, BattleCamp camp)
        {
            BattleScope scope = BattleScope.Instance;
            BattleCell cell = scope.GetCellR(x,z);
            scope.InitBattleUnit(role, cell,camp,camp.Index == 0);
        }

        public IBattleDescriptor Value => this;
    }
}