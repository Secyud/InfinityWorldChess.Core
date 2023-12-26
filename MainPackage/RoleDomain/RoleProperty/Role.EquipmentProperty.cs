#region

using InfinityWorldChess.ItemDomain;
using InfinityWorldChess.ItemDomain.EquipmentDomain;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Ugf.Collections.Generic;
using Secyud.Ugf.Archiving;

#endregion

namespace InfinityWorldChess.RoleDomain
{
    public partial class Role
    {
        public EquipmentProperty Equipment { get; } = new();

        public void SetEquipment(IEquipment equipment)
        {
            Equipment.Set(this, equipment);
        }


        public void AutoEquipEquipment()
        {
            List<IEquipment> equipments = Item.All().TryFindCast<IItem, IEquipment>();

            IEquipment ret = null;

            foreach (IEquipment equipment in equipments)
            {
                if ((ret?.Score ?? 0) < equipment.Score)
                {
                    ret = equipment;
                }
            }

            SetEquipment(ret);
        }

        [Guid("6E1D1802-EAA0-EBAE-53A8-FA078B1E010A")]
        public class EquipmentProperty
        {
            private IEquipment _equipment;

            public void Set(Role role, IEquipment equipment)
            {
                if (_equipment == equipment)
                    return;
                if (_equipment is not null)
                {
                    _equipment.UnInstallFrom(role);
                    role.Item.Add(_equipment);
                }

                _equipment = equipment;
                if (_equipment is not null)
                {
                    _equipment.InstallFrom(role);
                    role.Item.Remove(_equipment, 1);
                }
            }

            public IEquipment Get()
            {
                return _equipment;
            }

            public void Save(IArchiveWriter writer)
            {
                writer.WriteNullable(_equipment);
            }

            public void Load(IArchiveReader reader, Role role)
            {
                Set(role, reader.ReadNullable<IEquipment>());
            }
        }
    }
}