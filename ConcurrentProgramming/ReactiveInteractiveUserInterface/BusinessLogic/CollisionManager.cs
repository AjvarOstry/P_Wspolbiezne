using System.Collections.Concurrent;
using TP.ConcurrentProgramming.Data;

namespace TP.ConcurrentProgramming.BusinessLogic
{
    internal class CollisionManager
    {
        private readonly double _ballDiameter;
        private readonly object _lock = new();
        private readonly ConcurrentDictionary<IBall, IPosition> _positions
            = new();

        public double BallDiameter => _ballDiameter;
        internal CollisionManager(double ballDiameter)
        {
            _ballDiameter = ballDiameter;
        }

        internal void UpdatePosition(IBall ball, IPosition pos)
        {
            _positions[ball] = pos;

            foreach (var otherBall in _positions.Keys.ToArray()) 
            // przerobiłem list na array, bo jest bardziej odporne na to, jak jakaś
            // kulka będzie coś zmieniać w trakcie robienia snapshota akurat
            // i robimy to tylko po kluczu
            {
                if (ReferenceEquals(otherBall, ball)) continue;
                if (!_positions.TryGetValue(otherBall, out var otherPos)) continue;
                // tu se tę pozycję zczytujemy
                // na podstawie kulki
                // tak bezboleśnie, bez Runtimeów
                // bo pytamy o detale pojedynczo, a nie wszystko na raz
                // i nie wywala całej listy jak coś się rozjedzie
                if (IsColliding(pos, otherPos))
                    ResolveCollision(ball, otherBall);
            }
        }

        private bool IsColliding(IPosition a, IPosition b)
        {
            double dx = a.x - b.x;
            double dy = a.y - b.y;
            return Math.Sqrt(dx * dx + dy * dy) < _ballDiameter;
        }

        private void ResolveCollision(IBall a, IBall b)
        {
            Ball first = ((Ball)a).GetHashCode() < ((Ball)b).GetHashCode()
                ? (Ball)a
                : (Ball)b;

            Ball second = ReferenceEquals(first, a)
                ? (Ball)b
                : (Ball)a;

            lock (first.SyncRoot)
            {
                lock (second.SyncRoot)
                {
                    ResolveCollisionInternal(a, b);
                }
            }
        }
        private void ResolveCollisionInternal(IBall a, IBall b)
        {
            if (!_positions.TryGetValue(a, out var posA))
                return;

            if (!_positions.TryGetValue(b, out var posB))
                return;

            double relativeX = b.VelocityX - a.VelocityX;
            double relativeY = b.VelocityY - a.VelocityY;

            double diffX = posB.x - posA.x;
            double diffY = posB.y - posA.y;

            if ((relativeX * diffX + relativeY * diffY) >= 0)
                return;

            double oldVxA = a.VelocityX;
            double oldVyA = a.VelocityY;
            double oldVxB = b.VelocityX;
            double oldVyB = b.VelocityY;

            double totalMass = a.Mass + b.Mass;

            double newVxA =
                ((a.Mass - b.Mass) * a.VelocityX +
                 2 * b.Mass * b.VelocityX) / totalMass;

            double newVyA =
                ((a.Mass - b.Mass) * a.VelocityY +
                 2 * b.Mass * b.VelocityY) / totalMass;

            double newVxB =
                ((b.Mass - a.Mass) * b.VelocityX +
                 2 * a.Mass * a.VelocityX) / totalMass;

            double newVyB =
                ((b.Mass - a.Mass) * b.VelocityY +
                 2 * a.Mass * a.VelocityY) / totalMass;

            a.VelocityX = newVxA;
            a.VelocityY = newVyA;

            b.VelocityX = newVxB;
            b.VelocityY = newVyB;

            #region Diagnostic
            DataImplementation.Logger?.Log($"BALL | {a.GetHashCode()},{b.GetHashCode()} | BEFORE:A({oldVxA:F2},{oldVyA:F2}) B({oldVxB:F2},{oldVyB:F2}) | AFTER:A({newVxA:F2},{newVyA:F2}) B({newVxB:F2},{newVyB:F2})");
            #endregion
        }
    }
}