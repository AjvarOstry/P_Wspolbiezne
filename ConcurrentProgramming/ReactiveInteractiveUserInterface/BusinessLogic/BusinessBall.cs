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
    public Ball(Data.IBall ball)
    {
      ball.NewPositionNotification += RaisePositionChangeEvent;
    }

    #region IBall

    public event EventHandler<IPosition>? NewPositionNotification;

    #endregion IBall

    #region private

    private void RaisePositionChangeEvent(object? sender, Data.IVector e)
    {
      var dataBall = (Data.IBall)sender!;
      //40 średnica kulki
      
      double boxWidth  = 415 - 40; 
      double boxHeight = 435 - 40; 

      if (e.x <= 0 || e.x >= boxWidth)
      {
        dataBall.Velocity = new Vector(-dataBall.Velocity.x, dataBall.Velocity.y);
      }

      if (e.y <= 0 || e.y >= boxHeight) 
      {
        dataBall.Velocity = new Vector(dataBall.Velocity.x, -dataBall.Velocity.y);
      }

      NewPositionNotification?.Invoke(this, new Position(e.x, e.y));
    }

    #endregion private
  }
}