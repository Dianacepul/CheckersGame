namespace CheckersGame
{
    public enum CheckerColor { black = 1, white, none };

    public enum SquareColor { black = 1, white };

    public class Square
    {
        public CheckerColor checkerColor;
        public SquareColor squareColor;

        public Square(SquareColor squareColor, CheckerColor checkerColor)
        {
            this.squareColor = squareColor;
            this.checkerColor = checkerColor;
        }

        public object Clone()
        {
            return new Square(this.squareColor, this.checkerColor);
        }
    }
}