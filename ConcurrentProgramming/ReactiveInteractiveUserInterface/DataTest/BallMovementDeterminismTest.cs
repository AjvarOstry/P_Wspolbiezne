using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using TP.ConcurrentProgramming.Data;

namespace TP.ConcurrentProgramming.Data.Test
{
    [TestClass]
    public class BallsMoveDeterminismTest
    {
        [TestMethod]
        public void AllBalls_Should_Be_Updated_Consistently()
        {
            using (DataImplementation data = new DataImplementation())
            {
                int ballsCount = 10; // robimy ileś kulek

                data.Start(ballsCount, (_, __) => { });

                Thread.Sleep(200); // i sleep

                data.CheckBallsList(balls => // real sh*t
                {
                    var moves = balls.Select(b => b.GetMoveCount()).ToList(); // liczy ile każda kulka zrobiła updateów

                    int min = moves.Min();
                    int max = moves.Max(); //znajduje min i max

                    
                    Assert.IsTrue(max - min <= 1, "Balls are losing updates"); // mogą się różnić o 1, bo stopujemy w losowym momencie

                    Assert.IsTrue(min > 0, "Some haven't even moved :c"); // a w takim przypadku mamy przypał w ogóle
                });
            }
        }
    }
}