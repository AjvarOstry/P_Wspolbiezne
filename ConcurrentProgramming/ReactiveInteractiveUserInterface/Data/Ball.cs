using System.Diagnostics;

namespace TP.ConcurrentProgramming.Data
{
  internal class Ball : IBall
  {
    #region ctor

    internal Ball(Vector initialPosition, Vector initialVelocity, double mass)
    {
      Position = initialPosition;
      Velocity = initialVelocity;
      Mass = mass;
    }

    #endregion ctor

    #region IBall

    public event EventHandler<IVector>? NewPositionNotification;

    public IVector Velocity { get; set; }
    public double Mass { get; }
    
    public int GetMoveCount()
    {
      return MoveCount;
    }

    #endregion IBall

    #region private

    private Vector Position;
    private int MoveCount = 0;

    private void RaiseNewPositionChangeNotification()
    {
      NewPositionNotification?.Invoke(this, Position);
    }

    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    internal void Move(Vector velocity)
    {
      double dt = _stopwatch.Elapsed.TotalSeconds;
      _stopwatch.Restart();
      
      double dx = velocity.x * dt * 60; 
      double dy = velocity.y * dt * 60;
      
      Position = new Vector(Position.x + dx, Position.y + dy);
      
      MoveCount++; // to do tych testów mechaniki, że 10 kulek w 10 ticków robi 100 ruchów
      
      RaiseNewPositionChangeNotification();
    }

    #endregion private
  }
}