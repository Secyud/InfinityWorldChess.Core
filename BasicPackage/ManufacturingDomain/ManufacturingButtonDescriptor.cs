#region

using InfinityWorldChess.GameDomain.WorldCellDomain;
using InfinityWorldChess.ManufacturingDomain.Equipments;
using Secyud.Ugf;
using Secyud.Ugf.Archiving;
using Secyud.Ugf.DependencyInjection;
using Secyud.Ugf.TableComponents.ButtonComponents;

#endregion

namespace InfinityWorldChess.ManufacturingDomain
{
    public sealed class ManufacturingButtonDescriptor : ButtonDescriptor<WorldCell>
    {
        private static readonly string[] Names = { "铁匠铺", "餐馆", "药铺" };

        public int Type { get; set; }

        public override string Name => Names[Type];

        public override bool Visible(WorldCell target) => true;

        public override void Invoke()
        {
            IDependencyManager dm = U.M;
            switch (Type)
            {
                case 0:
                {
                    dm.CreateScope<EquipmentManufactureScope>();
                    break;
                }
                case 1:
                {
                    dm.CreateScope<EquipmentManufactureScope>();
                    break;
                }
                case 2:
                {
                    dm.CreateScope<EquipmentManufactureScope>();
                    break;
                }
            }
        }

        public void Save(IArchiveWriter writer)
        {
            writer.Write(Type);
        }

        public void Load(IArchiveReader reader)
        {
            Type = reader.ReadInt32();
        }
    }
}