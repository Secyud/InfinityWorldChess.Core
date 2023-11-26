namespace InfinityWorldChess.ItemDomain
{
    public class ItemCounter
    {
        public ItemCounter(IOverloadedItem item, int count)
        {
            Item = item;
            Count = count;
        }

        public IOverloadedItem Item { get; }
        public int Count { get; set; }
    }
}