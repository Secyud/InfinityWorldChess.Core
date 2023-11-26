namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragProcessContainer
    {
        public byte Position { get;}
        public bool Occupied { get; set; }
        // position
        public byte ProcessP { get; set; }
        // length
        public byte ProcessL { get; set; }
        public DragProcess Process { get; set; }

        public DragProcessContainer(byte position)
        {
            Position = position;
        }

        public void SetProcess(DragProcess process, byte startPosition)
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