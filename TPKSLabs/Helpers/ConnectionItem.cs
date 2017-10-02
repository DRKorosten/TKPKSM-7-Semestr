namespace TPKSLabs.Helpers
{
    public struct ConnectionItem
    {
        public int NodeFrom,
            NodeTo;

        public ConnectionItem(int from, int to)
        {
            NodeFrom = from;
            NodeTo = to;
        }
    }
}