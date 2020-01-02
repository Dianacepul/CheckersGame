using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CheckersGame.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly Board _board;
        private Board update;

        public ApiController(Board board)
        {
            _board = board;
        }

        [HttpGet]
        public ActionResult<Square[,]> Get()
        {
            var squares = _board.GetBoard();
            return squares;
        }

        [HttpPost]
        public ActionResult<GameState> Update(Turn turn)
        {
            var gameState = new GameState();
            if (_board.LegalMoves(_board, true).Any(lm => lm.XFrom == turn.XFrom && lm.YFrom == turn.YFrom && lm.XTo == turn.XTo && lm.YTo == turn.YTo))
            {
                update = _board.TakeTurn(turn);
                if (update.checkerBoard[turn.YTo, turn.XTo].checkerColor == CheckerColor.white && turn.YTo == 0)

                {
                    gameState.CheckerBoard = update.checkerBoard;
                    gameState.State = State.GameOver;
                    return gameState;
                }
                Node node = _board.SearchTree(update, 1, null, false);
                _board.GetScoresPreOrder(node, true);
                Node maxNode = Board.GetMaxNode(node);
                var newBoard = update.TakeTurn(maxNode.nodeTurn);

                if ((newBoard.checkerBoard[maxNode.nodeTurn.YTo, maxNode.nodeTurn.XTo].checkerColor == CheckerColor.black && maxNode.nodeTurn.YTo == 9) ||
                    (update.checkerBoard[turn.YTo, turn.XTo].checkerColor == CheckerColor.white && turn.YTo == 0))
                {
                    gameState.CheckerBoard = newBoard.checkerBoard;
                    gameState.State = State.GameOver;
                    return gameState;
                }
                _board.checkerBoard = newBoard.checkerBoard;
                _board.GetBlackCount();
                _board.GetWhiteCount();
                if (_board.GetBlackCount() == 0 || _board.GetWhiteCount() == 0)
                {
                    gameState.CheckerBoard = newBoard.checkerBoard;
                    gameState.State = State.GameOver;
                    return gameState;
                }

                gameState.CheckerBoard = newBoard.checkerBoard;
                gameState.State = State.Game;
            }
            else
            {
                gameState.CheckerBoard = _board.checkerBoard;
                gameState.State = State.Restart;
            }

            return gameState;
        }

        [HttpGet]
        public ActionResult<int> GetWhiteCount()
        {
            return _board.GetWhiteCount();
        }

        [HttpGet]
        public ActionResult<int> GetBlackCount()
        {
            return _board.GetBlackCount();
        }
    }
}