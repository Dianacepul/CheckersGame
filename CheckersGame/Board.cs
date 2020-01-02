using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersGame
{
    public class Board : ICloneable
    {
        public Square[,] checkerBoard;
        private const int MAX = 10000;
        private const int MIN = -10000;
        private int scores;
        public List<Turn> legalMoves;
        public Turn Turn { get; set; }

        public Board()
        {
            checkerBoard = new Square[10, 10];
        }

        public Square[,] GetBoard()
        {
            for (int i = 0; i < checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < checkerBoard.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        checkerBoard[i, j] = new Square(SquareColor.white, CheckerColor.none);
                    }
                    else
                    {
                        if (i <= 3)
                        {
                            checkerBoard[i, j] = new Square(SquareColor.black, CheckerColor.black);
                        }
                        else if (i >= 6)
                        {
                            checkerBoard[i, j] = new Square(SquareColor.black, CheckerColor.white);
                        }
                        else
                        {
                            checkerBoard[i, j] = new Square(SquareColor.black, CheckerColor.none);
                        }
                    }
                }
            }
            return checkerBoard;
        }

        public object Clone()
        {
            var result = new Board() { checkerBoard = new Square[10, 10] };
            for (int i = 0; i < checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < checkerBoard.GetLength(1); j++)
                {
                    result.checkerBoard[i, j] = (Square)checkerBoard[i, j].Clone();
                }
            }
            return result;
        }

        public Board TakeTurn(Turn turn)
        {
            Board copyCheckerBoard = (Board)Clone();

            int jTo = turn.XTo;
            int iTo = turn.YTo;
            int iFrom = turn.YFrom;
            int jFrom = turn.XFrom;
            int xResult = Math.Abs(iTo - iFrom);
            int yResult = Math.Abs(jTo - jFrom);
            int xBetween = Math.Abs((jTo + jFrom) / 2);
            int yBetween = Math.Abs((iTo + iFrom) / 2);

            if (copyCheckerBoard.checkerBoard[iTo, jTo].checkerColor == CheckerColor.none && copyCheckerBoard.checkerBoard[iFrom, jFrom].squareColor == SquareColor.black
                && copyCheckerBoard.checkerBoard[iFrom, jFrom].checkerColor != CheckerColor.black && xResult == 1)
            {
                copyCheckerBoard.checkerBoard[iFrom, jFrom] = new Square(SquareColor.black, CheckerColor.none);
                copyCheckerBoard.checkerBoard[iTo, jTo] = new Square(SquareColor.black, CheckerColor.white);
            }
            else if (copyCheckerBoard.checkerBoard[iTo, jTo].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard[iTo, jTo].checkerColor == CheckerColor.none && yResult == 2
            && copyCheckerBoard.checkerBoard[iFrom, jFrom].checkerColor == CheckerColor.white && copyCheckerBoard.checkerBoard[iFrom, jFrom].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard[yBetween, xBetween].checkerColor == CheckerColor.black && copyCheckerBoard.checkerBoard[yBetween, xBetween].squareColor == SquareColor.black)
            {
                copyCheckerBoard.checkerBoard[iFrom, jFrom] = new Square(SquareColor.black, CheckerColor.none);
                copyCheckerBoard.checkerBoard[iTo, jTo] = new Square(SquareColor.black, CheckerColor.white);
                copyCheckerBoard.checkerBoard[yBetween, xBetween] = new Square(SquareColor.black, CheckerColor.none);
            }
            else if (copyCheckerBoard.checkerBoard[iTo, jTo].checkerColor == CheckerColor.none && copyCheckerBoard.checkerBoard[iFrom, jFrom].squareColor == SquareColor.black
                 && copyCheckerBoard.checkerBoard[iFrom, jFrom].checkerColor == CheckerColor.black && xResult == 1)
            {
                copyCheckerBoard.checkerBoard[iFrom, jFrom] = new Square(SquareColor.black, CheckerColor.none);
                copyCheckerBoard.checkerBoard[iTo, jTo] = new Square(SquareColor.black, CheckerColor.black);
            }
            else if (copyCheckerBoard.checkerBoard[iTo, jTo].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard[iTo, jTo].checkerColor == CheckerColor.none && yResult == 2
            && copyCheckerBoard.checkerBoard[iFrom, jFrom].checkerColor == CheckerColor.black && copyCheckerBoard.checkerBoard[iFrom, jFrom].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard[yBetween, xBetween].checkerColor == CheckerColor.white && copyCheckerBoard.checkerBoard[yBetween, xBetween].squareColor == SquareColor.black)
            {
                copyCheckerBoard.checkerBoard[iFrom, jFrom] = new Square(SquareColor.black, CheckerColor.none);
                copyCheckerBoard.checkerBoard[iTo, jTo] = new Square(SquareColor.black, CheckerColor.black);
                copyCheckerBoard.checkerBoard[yBetween, xBetween] = new Square(SquareColor.black, CheckerColor.none);
            }

            return copyCheckerBoard;
        }

        public List<Turn> LegalMoves(Board copyCheckerBoard, bool whiteMove)
        {
            legalMoves = new List<Turn>();
            for (int i = 0; i < copyCheckerBoard.checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < copyCheckerBoard.checkerBoard.GetLength(1); j++)

                {
                    if (!whiteMove && copyCheckerBoard.checkerBoard[i, j].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard[i, j].checkerColor == CheckerColor.black &&
                        copyCheckerBoard.checkerBoard.GetLength(0) > i && copyCheckerBoard.checkerBoard.GetLength(1) > j)
                    {
                        if (copyCheckerBoard.checkerBoard.GetLength(0) > i + 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j + 1 &&
                            copyCheckerBoard.checkerBoard[i + 1, j + 1].checkerColor == CheckerColor.none)
                        {
                            legalMoves.Add(new Turn(j, i, j + 1, i + 1));
                        }
                        if (0 <= j - 1 &&
                          copyCheckerBoard.checkerBoard.GetLength(0) > i + 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 1 &&
                          copyCheckerBoard.checkerBoard[i + 1, j - 1].checkerColor == CheckerColor.none)
                        {
                            legalMoves.Add(new Turn(j, i, j - 1, i + 1));
                        }
                        if (9 >= i + 2 && 9 >= j + 2 && 9 >= i + 1 && 9 >= j + 1 &&
                               copyCheckerBoard.checkerBoard[i + 2, j + 2].checkerColor == CheckerColor.none && copyCheckerBoard.checkerBoard[i + 1, j + 1].checkerColor == CheckerColor.white)

                        {
                            legalMoves.Add(new Turn(j, i, j + 2, i + 2));
                        }
                        if (0 <= i + 2 && 0 <= j - 2 && 0 <= i + 1 && 0 <= j - 1 &&
                            copyCheckerBoard.checkerBoard.GetLength(0) > i + 2 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 2 &&
                            copyCheckerBoard.checkerBoard.GetLength(0) > i + 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 1 &&
                            copyCheckerBoard.checkerBoard[i + 2, j - 2].checkerColor == CheckerColor.none && copyCheckerBoard.checkerBoard[i + 1, j - 1].checkerColor == CheckerColor.white
                            )
                        {
                            legalMoves.Add(new Turn(j, i, j - 2, i + 2));
                        }
                    }
                    if (whiteMove == true && copyCheckerBoard.checkerBoard[i, j].checkerColor == CheckerColor.white && copyCheckerBoard.checkerBoard[i, j].squareColor == SquareColor.black && copyCheckerBoard.checkerBoard.GetLength(0) > i && copyCheckerBoard.checkerBoard.GetLength(1) > j)
                    {
                        if (0 <= i - 1 && 0 <= j + 1 &&
                           copyCheckerBoard.checkerBoard.GetLength(0) > i - 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j + 1 &&
                           copyCheckerBoard.checkerBoard[i - 1, j + 1].checkerColor == CheckerColor.none)

                        {
                            legalMoves.Add(new Turn(j, i, j + 1, i - 1));
                        }
                        if (0 <= i - 1 && 0 <= j - 1 &&
                           copyCheckerBoard.checkerBoard.GetLength(0) > i - 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 1 &&
                           copyCheckerBoard.checkerBoard[i - 1, j - 1].checkerColor == CheckerColor.none
                           )
                        {
                            legalMoves.Add(new Turn(j, i, j - 1, i - 1));
                        }
                        if (0 <= i - 2 && 0 <= j - 2 && 0 <= i - 1 && 0 <= j - 1 &&
                             copyCheckerBoard.checkerBoard.GetLength(0) > i - 2 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 2 &&
                             copyCheckerBoard.checkerBoard.GetLength(0) > i - 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j - 1 &&
                             copyCheckerBoard.checkerBoard[i - 1, j - 1].checkerColor == CheckerColor.black && copyCheckerBoard.checkerBoard[i - 2, j - 2].checkerColor == CheckerColor.none)

                        {
                            legalMoves.Add(new Turn(j, i, j - 2, i - 2));
                        }
                        if (0 <= i - 2 && 0 <= j + 2 && 0 <= i - 1 && 0 <= j + 1 &&
                                copyCheckerBoard.checkerBoard.GetLength(0) > i - 2 && copyCheckerBoard.checkerBoard.GetLength(1) > j + 2 &&
                                copyCheckerBoard.checkerBoard.GetLength(0) > i - 1 && copyCheckerBoard.checkerBoard.GetLength(1) > j + 1 &&
                                copyCheckerBoard.checkerBoard[i - 1, j + 1].checkerColor == CheckerColor.black && copyCheckerBoard.checkerBoard[i - 2, j + 2].checkerColor == CheckerColor.none)

                        {
                            legalMoves.Add(new Turn(j, i, j + 2, i - 2));
                        }
                    }
                }
            }
            return legalMoves;
        }

        public Node SearchTree(Board copyCheckerBoard, int depht, Node node, bool whiteMove)
        {
            if (depht <= 2)
            {
                if (node == null)
                {
                    node = new Node();
                }
                node.Nodes = LegalMoves(copyCheckerBoard, whiteMove).Select(m => new Node { nodeTurn = m }).ToList();
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    var updatedCheckerBoard = TakeTurn(node.Nodes[i].nodeTurn);
                    SearchTree(updatedCheckerBoard, depht + 1, node.Nodes[i], !whiteMove);
                }
            }

            return node;
        }

        public int GetHeuristicScores(Turn turn)
        {
            for (int i = 0; i < checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < checkerBoard.GetLength(1); j++)
                {
                    if (checkerBoard[turn.YFrom, turn.XFrom].checkerColor == CheckerColor.black)
                    {
                        if (turn.YTo == 0)
                        {
                            scores = 1;
                        }
                        else if (turn.YTo == 1)
                        {
                            scores = 2;
                        }
                        else if (turn.YTo == 2)
                        {
                            scores = 3;
                        }
                        else if (turn.YTo == 3)
                        {
                            scores = 4;
                        }
                        else if (turn.YTo == 4)
                        {
                            scores = 5;
                        }
                        else if (turn.YTo == 5)
                        {
                            scores = 6;
                        }
                        else if (turn.YTo == 6)
                        {
                            scores = 7;
                        }
                        else if (turn.YTo == 7)
                        {
                            scores = 8;
                        }
                        else if (turn.YTo == 8)
                        {
                            scores = 9;
                        }
                        else if (turn.YTo == 9)
                        {
                            scores = 10;
                        }
                    }
                    if (checkerBoard[turn.YFrom, turn.XFrom].checkerColor == CheckerColor.white)
                    {
                        if (turn.YTo == 9)
                        {
                            scores = -10;
                        }
                        else if (turn.YTo == 8)
                        {
                            scores = -9;
                        }
                        else if (turn.YTo == 7)
                        {
                            scores = -8;
                        }
                        else if (turn.YTo == 6)
                        {
                            scores = -7;
                        }
                        else if (turn.YTo == 5)
                        {
                            scores = -6;
                        }
                        else if (turn.YTo == 6)
                        {
                            scores = -7;
                        }
                        else if (turn.YTo == 7)
                        {
                            scores = -8;
                        }
                        else if (turn.YTo == 8)
                        {
                            scores = -9;
                        }
                        else if (turn.YTo == 9)
                        {
                            scores = -10;
                        }
                    }
                }
            }
            return scores;
        }

        public void GetScoresPreOrder(Node node, bool whiteMove)
        {
            if (node.Nodes != null && node.Nodes.Any())
            {
                foreach (var n in node.Nodes)
                {
                    GetScoresPreOrder(n, !whiteMove);
                }

                node.Scores = whiteMove ? node.Nodes.Min(n => n.Scores) : node.Nodes.Max(n => n.Scores);
            }
            else
            {
                node.Scores = GetHeuristicScores(node.nodeTurn);
            }
        }

        public static Node GetMaxNode(Node node)
        {
            var maxNodeScore = node.Nodes.OrderBy(m => m.Scores).Last();

            return maxNodeScore;
        }

        public int GetWhiteCount()
        {
            int countWhite = 0;
            for (int i = 0; i < checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < checkerBoard.GetLength(1); j++)
                {
                    if (checkerBoard[i, j].checkerColor == CheckerColor.white)
                    {
                        countWhite++;
                    }
                }
            }

            return countWhite;
        }

        public int GetBlackCount()
        {
            int countBlack = 0;
            for (int i = 0; i < checkerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < checkerBoard.GetLength(1); j++)
                {
                    if (checkerBoard[i, j].checkerColor == CheckerColor.black)
                    {
                        countBlack++;
                    }
                }
            }
            return countBlack;
        }
    }
}