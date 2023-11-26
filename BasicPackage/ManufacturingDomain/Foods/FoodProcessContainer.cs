namespace InfinityWorldChess.ManufacturingDomain.Foods
{
    public class FoodProcessContainer
    {
        public byte Position { get;}
        public bool Occupied { get; set; }
        // position
        public byte ProcessP { get; set; }
        // length
        public byte ProcessL { get; set; }
        public FoodProcess Process { get; set; }

        public FoodProcessContainer(byte position)
        {
            Position = position;
        }

        public void SetProcess(FoodProcess process, byte startPosition)
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