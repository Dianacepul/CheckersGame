namespace CheckersGame
{
    public class Turn
    {
        public int XFrom { get; set; }
        public int YFrom { get; set; }
        public int XTo { get; set; }
        public int YTo { get; set; }

        public Turn(int XFrom, int YFrom, int XTo, int YTo)
        {
            this.XFrom = XFrom;
            this.YFrom = YFrom;
            this.XTo = XTo;
            this.YTo = YTo;
        }

        public Turn()
        {
        }
    }
}