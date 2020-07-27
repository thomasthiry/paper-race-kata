using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace PaperRaceKata
{
    public class InertiaBehaviorTest
    {
        [Fact]
        public void WhenAdjustCenterTheInertiaOfTheCarStaysTheSame()
        {
            var car = new Car();
            var initialInertia = car.Inertia;
            car.Adjust(Adjustment.Center);
            Assert.Equal(initialInertia, car.Inertia);
        }

        [Fact]
        public void NewCarHasNoInertia()
        {
            var car = new Car();
            Assert.Equal(new Vector(0,0), car.Inertia);
        }

        [Theory]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.East, 1, 0)]
        [InlineData(Adjustment.NorthEast, 1, 1)]
        public void WhenAdjustingInADirectionTheInertiaOfTheCarPullsInThatDirection(Adjustment adjustment, int x, int y)
        {
            var car = new Car();
            car.Adjust(adjustment);
            Assert.Equal(new Vector(x,y), car.Inertia);
        }
    }

    public enum Adjustment
    {
        Center,
        East,
        West,
        NorthEast
    }

    public class Car
    {
        public Vector Inertia { get; private set; } = new Vector(0, 0);

        public void Adjust(Adjustment adjustment)
        {
            Inertia = Vector.Adjust(adjustment);
        }
    }

    public class Vector : IEquatable<Vector>
    {
        private readonly int _x;
        private readonly int _y;

        public static Vector Adjust(Adjustment adjustment)
        {
            return adjustment switch
            {
                Adjustment.East => new Vector(1, 0),
                Adjustment.West => new Vector(-1, 0),
                Adjustment.NorthEast => new Vector(1, 1),
                _ => new Vector(0, 0)
            };
        }

        public Vector(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public bool Equals(Vector other)
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
            return Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !Equals(left, right);
        }
    }
}
