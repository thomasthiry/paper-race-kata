using System;

namespace PaperRaceKata
{
    public class Inertia : IEquatable<Inertia>
    {
        private readonly int _x;
        private readonly int _y;

        public static Inertia DirectionFor(Adjustment adjustment)
        {
            return adjustment switch
            {
                Adjustment.East => new Inertia(1, 0),
                Adjustment.West => new Inertia(-1, 0),
                Adjustment.NorthEast => new Inertia(1, 1),
                _ => new Inertia(0, 0)
            };
        }

        public Inertia(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(Inertia other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Inertia) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }

        public static bool operator ==(Inertia left, Inertia right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Inertia left, Inertia right)
        {
            return !Equals(left, right);
        }

        public Inertia Add(Inertia other)
        {
            return new Inertia(this._x + other._x, this._y + other._y); 
        }
    }
}