//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.Data
{
  internal class Ball : IBall
  {
    #region ctor

    internal Ball(Vector initialPosition, Vector initialVelocity)
    {
      Position = initialPosition;
      Velocity = initialVelocity;
    }

    #endregion ctor

    #region IBall

    public event EventHandler<IVector>? NewPositionNotification;

    public IVector Velocity { get; set; }

    #endregion IBall

    #region private

    private Vector Position;

    private void RaiseNewPositionChangeNotification()
    {
      NewPositionNotification?.Invoke(this, Position);
    }

    internal void Move(Vector delta, Vector positionLimiter)
    {
      double dx = delta.x;
      double dy = delta.y;
      
      if (Position.x + dx >= positionLimiter.x || Position.x + dx <= 0)
        dx = -dx;
      if (Position.y + dy >= positionLimiter.y || Position.y + dy <= 0)
        dy = -dy;
      
      Position = new Vector(Position.x + dx, Position.y + dy);
      RaiseNewPositionChangeNotification();
    }

    #endregion private
  }
}