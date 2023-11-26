namespace InfinityWorldChess.ManufacturingDomain.Drags
{
    public class DragMaterialContainer
    {
        public byte Position { get;}
        public bool Occupied { get; set; }
        // position
        public byte MaterialP { get; set; }
        // length
        public byte MaterialL { get; set; }
        public DragMaterial Material { get; set; }

        public DragMaterialContainer(byte position)
        {
            Position = position;
        }

        public void SetProcess(DragMaterial process, byte startPosition)
        {
            if (startPosition == Position)
            {
                Material = process;
            }

            if (process is null)
            {
                MaterialL = 0;
                MaterialP = 0;
                Occupied = false;
            }
            else
            {
                
                MaterialL = process.Length;
                MaterialP = startPosition;
                Occupied = true;
            }
        }
    }
}