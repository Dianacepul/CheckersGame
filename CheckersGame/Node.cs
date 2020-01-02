using System.Collections.Generic;

namespace CheckersGame
{
    public class Node
    {
        public Turn nodeTurn { get; set; }
        public List<Node> Nodes { get; set; }
        public int Scores { get; set; }

        public Node Parent { get; set; }
    }
}