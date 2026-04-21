//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.BusinessLogic.Test
{
  [TestClass]
  public class BallUnitTest
  {
    [TestMethod]
    public void MoveTestMethod()
    {
      DataBallFixture dataBallFixture = new DataBallFixture();
      Ball newInstance = new(dataBallFixture);
      int numberOfCallBackCalled = 0;
      newInstance.NewPositionNotification += (sender, position) => { Assert.IsNotNull(sender); Assert.IsNotNull(position); numberOfCallBackCalled++; };
      dataBallFixture.Move();
      Assert.AreEqual<int>(1, numberOfCallBackCalled);
    }

    [TestMethod]
    public void CollisionWallTestMethod()
    {
      DataBallFixture dataBallFixture;
      //Prawa
      dataBallFixture = CollisionTestHelper(vx: 20, vy: 0, x: 380, y: 200);
      
      Assert.AreEqual(-20, dataBallFixture.Velocity.x, 0.01);
      Assert.AreEqual(0, dataBallFixture.Velocity.y, 0.01);
      
      
      //Lewa
      dataBallFixture = CollisionTestHelper(vx: -20, vy: 0, x: 0, y: 200);
      
      Assert.AreEqual(20, dataBallFixture.Velocity.x, 0.01);
      Assert.AreEqual(0, dataBallFixture.Velocity.y, 0.01);
      
      
      //Górna
      dataBallFixture = CollisionTestHelper(vx: 0, vy: 20, x: 200, y: 400);
      
      Assert.AreEqual(-20, dataBallFixture.Velocity.y, 0.01);
      Assert.AreEqual(0, dataBallFixture.Velocity.x, 0.01);
      
      
      //Dolna
      dataBallFixture = CollisionTestHelper(vx: 0, vy: -20, x: 200, y: 0);
      
      Assert.AreEqual(20, dataBallFixture.Velocity.y, 0.01);
      Assert.AreEqual(0, dataBallFixture.Velocity.x, 0.01);
      
      
      //lewy górny róg
      dataBallFixture = CollisionTestHelper(vx: -20, vy: -20, x: 0, y: 0);
      
      Assert.AreEqual(20, dataBallFixture.Velocity.y, 0.01);
      Assert.AreEqual(20, dataBallFixture.Velocity.x, 0.01);
    }

    private DataBallFixture CollisionTestHelper(double vx, double vy, double x, double y)
    {
      DataBallFixture dataBallFixture = new DataBallFixture(vx, vy);
      Ball newInstance = new(dataBallFixture);

      newInstance.NewPositionNotification += (sender, position) => { };
      dataBallFixture.Move(x, y);
      
      return dataBallFixture;
    }

    [TestMethod]
    public void CollisionCornerTestMethod()
    {
      DataBallFixture dataBallFixture = new DataBallFixture(vx: 20, vy: 20);
      Ball newInstance = new(dataBallFixture);

      newInstance.NewPositionNotification += (sender, position) => { };
      dataBallFixture.Move(x: 410, y: 410); 

      Assert.IsTrue(dataBallFixture.Velocity.x < 0);
      Assert.IsTrue(dataBallFixture.Velocity.y < 0);
    }
    
    #region testing instrumentation

    private class DataBallFixture : Data.IBall
    {
      public Data.IVector Velocity { get; set; }

      public event EventHandler<Data.IVector>? NewPositionNotification;

      internal DataBallFixture(double vx = 0, double vy = 0)
      {
        Velocity = new VectorFixture(vx, vy);
      }
      
      internal void Move(double x = 0.0, double y = 0.0)
      {
        NewPositionNotification?.Invoke(this, new VectorFixture(x, y));
      }
    }

    private class VectorFixture : Data.IVector
    {
      internal VectorFixture(double X, double Y)
      {
        x = X; y = Y;
      }

      public double x { get; init; }
      public double y { get; init; }
    }

    #endregion testing instrumentation
  }
}