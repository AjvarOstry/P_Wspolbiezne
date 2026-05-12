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

    #endregion IBall

    #region private

    private Vector Position;

    private void RaiseNewPositionChangeNotification()
    {
      NewPositionNotification?.Invoke(this, Position);
    }

    internal void Move(Vector delta)
    {
      double dx = delta.x;
      double dy = delta.y;
      
      Position = new Vector(Position.x + dx, Position.y + dy);
      RaiseNewPositionChangeNotification();
    }

    #endregion private
  }
}