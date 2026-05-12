//____________________________________________________________________________________________________________________________________
//
//  Copyright (C) 2024, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and get started commenting using the discussion panel at
//
//  https://github.com/mpostol/TP/discussions/182
//
//_____________________________________________________________________________________________________________________________________

namespace TP.ConcurrentProgramming.BusinessLogic
{
  internal class Ball : IBall
  {
    private readonly Data.Box _box;

    public Ball(Data.IBall ball, Data.DataAbstractAPI data)
    {
      _box = new Data.Box(data.BoxWidth, data.BoxHeight);
      ball.NewPositionNotification += RaisePositionChangeEvent;
    }

    private void RaisePositionChangeEvent(object? sender, Data.IVector e)
    {
      var dataBall = (Data.IBall)sender!;

      if (_box.HitsVerticalWall(e.x))
        dataBall.Velocity = new Vector(-dataBall.Velocity.x, dataBall.Velocity.y);

      if (_box.HitsHorizontalWall(e.y))
        dataBall.Velocity = new Vector(dataBall.Velocity.x, -dataBall.Velocity.y);

      NewPositionNotification?.Invoke(this, new Position(e.x, e.y));
    }

    public event EventHandler<IPosition>? NewPositionNotification;
  }
}