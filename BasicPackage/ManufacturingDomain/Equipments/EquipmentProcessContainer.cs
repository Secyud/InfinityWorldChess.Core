namespace InfinityWorldChess.ManufacturingDomain.Equipments
{
    public class EquipmentProcessContainer
    {
        public byte Position { get;}
        public bool Occupied { get; set; }
        // position
        public byte ProcessP { get; set; }
        // length
        public byte ProcessL { get; set; }
        public EquipmentProcess Process { get; set; }

        public EquipmentProcessContainer(byte position)
        {
            Position = position;
        }

        public void SetProcess(EquipmentProcess process, byte startPosition)
        {
            if (startPosition == Position)
            {
                Process = process;
            }

            if (process is null)
            {
                ProcessL = 0;
                ProcessP = 0;
                Occupied = false;
            }
            else
            {
                
                ProcessL = process.Length;
                ProcessP = startPosition;
                Occupied = true;
            }
        }
    }
}