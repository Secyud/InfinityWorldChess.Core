#region

using InfinityWorldChess.BattleDomain;
using InfinityWorldChess.BuffDomain;
using InfinityWorldChess.RoleDomain;
using InfinityWorldChess.Ugf;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace InfinityWorldChess.ItemDomain.FoodDomain
{
    public sealed class Food : BuffProperty<Food>,
        IItem, IEdible, IEdibleInBattle, IHasFlavor, IHasMouthfeel, IArchivableShown, IArchivable
    {
        public readonly List<IBuff<Role>> RoleBuffs = new();
        public readonly List<IBuff<BattleRole>> BattleRoleBuffs = new();


        public string Name { get; set; }

        public string Description { get; set; }

        public IObjectAccessor<Sprite> Icon { get; set; }

        public string ShowName => "菜肴 " + Name;

        public string ShowDescription => Description;

        public IObjectAccessor<Sprite> ShowIcon => Icon;

        public byte Score { get; set; }

        public int SaveIndex { get; set; }

        protected override Food Target => this;

        public float[] FlavorLevel { get; } = new float[BasicConsts.FlavorCount];
        public float[] MouthFeelLevel { get; }= new float[BasicConsts.MouthFeelCount];

        public void Eating(Role role)
        {
            foreach (IBuff<Role> buff in RoleBuffs)
                buff.Install(role);
        }

        public void EatingInBattle(BattleRole role)
        {
            foreach (IBuff<BattleRole> buff in BattleRoleBuffs)
                buff.Install(role);
        }

        public override void Save(IArchiveWriter writer)
        {
            this.SaveShown(writer);
            this.SaveMouthFeel(writer);
            this.SaveFlavors(writer);
            base.Save(writer);
        }

        public override void Load(IArchiveReader reader)
        {
            this.LoadShown(reader);
            this.LoadMouthFeel(reader);
            this.LoadFlavors(reader);
            base.Load(reader);
        }

        public void SetContent(Transform transform)
        {
            transform.AddItemHeader(this);
            transform.AddFlavorInfo(this);
            transform.AddMouthFeelInfo(this);
            transform.AddListShown("效果", Values);
        }

    }
}