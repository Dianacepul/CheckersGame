namespace CheckersGame
{
    public enum State
    { Game, GameOver, Restart };

    public class GameState
    {
        public State State { get; set; }

        public Square[,] CheckerBoard { get; set; }
    }
}