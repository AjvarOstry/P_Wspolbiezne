//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.Data.Test
{
  [TestClass]
  public class BallUnitTest
  {
    [TestMethod]
    public void ConstructorTestMethod()
    {
      Vector testinVector = new Vector(0.0, 0.0);
      Ball newInstance = new(testinVector, testinVector);
    }

    [TestMethod]
    public void MoveOnBoundaryTestMethod()
    {

      Vector positionLimiter = new Vector(400, 400);
      int callbackCount = 0;

      void RunTest(Vector initialPosition, Vector velocity)
      {
        Ball ball = new Ball(initialPosition, velocity);

        IVector currentPosition = null;

        ball.NewPositionNotification += (sender, position) =>
        {
          currentPosition = position;
          callbackCount++;
        };

        Vector delta = new Vector(velocity.x, velocity.y);

        ball.Move(delta, positionLimiter);

        Assert.IsNotNull(currentPosition);

        Assert.IsTrue(currentPosition.x >= 0 && currentPosition.x <= positionLimiter.x);
        Assert.IsTrue(currentPosition.y >= 0 && currentPosition.y <= positionLimiter.y);
      }


      RunTest(new Vector(390, 200), new Vector(20, 0));

      RunTest(new Vector(10, 200), new Vector(-20, 0));

      RunTest(new Vector(200, 390), new Vector(0, 20));

      RunTest(new Vector(200, 10), new Vector(0, -20));
      
      Assert.AreEqual(4, callbackCount);
    }

    [TestMethod]
    public void MoveCornerCollisionTestMethod()
    {
      Vector positionLimiter = new Vector(400, 400);

      Vector initialPosition = new Vector(390, 390);
      
      Vector velocity = new Vector(20, 20);

      Ball ball = new Ball(initialPosition, velocity);

      IVector currentPosition = null;
      int callbackCount = 0;

      ball.NewPositionNotification += (sender, position) =>
      {
        currentPosition = position;
        callbackCount++;
      };

      Vector delta = new Vector(velocity.x, velocity.y);
      
      ball.Move(delta, positionLimiter);
      
      Assert.IsNotNull(currentPosition);
      Assert.AreEqual(1, callbackCount);
      
      Assert.IsTrue(currentPosition.x <= positionLimiter.x);
      Assert.IsTrue(currentPosition.y <= positionLimiter.y);

      // powinna zmienić kierunek (odbicie)
      Assert.IsTrue(Math.Abs(currentPosition.x - (initialPosition.x - delta.x)) < 0.01 &&
                    Math.Abs(currentPosition.y - (initialPosition.y - delta.y)) < 0.01);
    }
  }

}