using System.Collections.Generic;
using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.GameDomain;
using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.RoleDomain;
using Secyud.Ugf;
using Secyud.Ugf.DataManager;
using Secyud.Ugf.HexMap;
using UnityEngine;

namespace InfinityWorldChess.BattleTemplates
{
    public class Battle : IBattleDescriptor,IObjectAccessor<IBattleDescriptor>
    {
        [field: S]public string ResourceId { get; set; }
        [field: S] public int SizeX { get; set; }
        [field: S] public int SizeZ { get; set; }
        [field: S] public IBattleVictoryCondition VictoryCondition { get; set; }
        [field: S] public string Description { get;set; }
        [field: S] public string Name { get; set;}
        [field: S] public IObjectAccessor<Sprite> Icon { get; set;}
        [field: S] public List<BattleCampSetting> BattleCampSettings { get; } = new();
        public virtual WorldCell Cell => GameScope.Instance.Player.Role.Position;
        
        public void OnBattleFinished()
        {
            
        }

        public void OnBattleCreated()
        {
            foreach (BattleCampSetting setting in BattleCampSettings)
            {
                BattleCamp camp = new()
                {
                    Name = setting.Name,
                    Index = setting.Index,
                    Color = new Color(setting.R,setting.G,setting.B)
                };

                foreach (RoleWithIndex roleIndex in setting.CampRoles)
                {
                    Role role = roleIndex.RoleAccessor?.Value;
                    if (role is not null)
                    {
                        AddBattleRole(role, roleIndex.Index, camp);
                    }
                }
            }
            
            VictoryCondition.SetCondition();
        }

        public bool Victory => VictoryCondition.Victory;
        public bool Defeated => VictoryCondition.Defeated;

        private static void AddBattleRole(Role role,int index,BattleCamp camp)
        {
            BattleScope scope = BattleScope.Instance;
            HexCell cell = scope.Map.GetCell(index);
            BattleRole battleRole = new(role)
            {
                PlayerControl = true,
                Camp = camp
            };
            scope.AddRoleBattleChess(battleRole, cell);
        }

        public IBattleDescriptor Value => this;
    }
}