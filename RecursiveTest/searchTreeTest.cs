using CheckersGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace RecursiveTest
{
    [TestClass]
    public class SearchTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Board tree = new Board();
            List<Turn> legalMoves = tree.LegalMoves(tree, true);
            Assert.IsTrue(legalMoves.Any(m => m.XFrom == 1 && m.YFrom == 6 && m.XTo == 2 && m.YTo == 5));
        }
    }
}