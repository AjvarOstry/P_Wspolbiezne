namespace TP.ConcurrentProgramming.BusinessLogic
{
  internal class Ball : IBall
  {
    private readonly Data.Box _box;
    private readonly Data.IBall _dataBall;
    private readonly CollisionManager _collisionManager;

    public Ball(Data.IBall ball, Data.DataAbstractAPI data, CollisionManager collisionManager)
    {
      _dataBall = ball;
      _box = new Data.Box(data.BoxWidth, data.BoxHeight);
      _collisionManager = collisionManager;
      ball.NewPositionNotification += RaisePositionChangeEvent;
    }


    public double Mass => _dataBall.Mass;
    
    public double VelocityX 
    { 
      get => _dataBall.Velocity.x; 
      set => _dataBall.Velocity = new Vector(value, _dataBall.Velocity.y); 
    }

    public double VelocityY 
    { 
      get => _dataBall.Velocity.y; 
      set => _dataBall.Velocity = new Vector(_dataBall.Velocity.x, value); 
    }

    private void RaisePositionChangeEvent(object? sender, Data.IVector e)
    {
      var dataBall = (Data.IBall)sender!;

      if (_box.HitsVerticalWall(e.x))
        dataBall.Velocity = new Vector(-dataBall.Velocity.x, dataBall.Velocity.y);

      if (_box.HitsHorizontalWall(e.y))
        dataBall.Velocity = new Vector(dataBall.Velocity.x, -dataBall.Velocity.y);

      var pos = new Position(e.x, e.y);
      _collisionManager.UpdatePosition(this, pos);
      NewPositionNotification?.Invoke(this, pos);
    }

    public event EventHandler<IPosition>? NewPositionNotification;
  }
}