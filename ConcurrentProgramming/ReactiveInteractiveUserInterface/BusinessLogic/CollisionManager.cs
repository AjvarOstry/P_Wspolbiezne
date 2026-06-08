using System.Collections.Concurrent;

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

            foreach (var (other, otherPos) in _positions.ToList()) // snapshot!
            {
                if (ReferenceEquals(other, ball)) continue;
                if (IsColliding(pos, otherPos))
                    ResolveCollision(ball, other);
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
        }
    }
}