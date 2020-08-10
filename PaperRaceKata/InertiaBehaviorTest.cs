using System;
using System.Collections.Generic;
using Xunit;

namespace PaperRaceKata
{
    public class InertiaBehaviorTest
    {
        [Fact]
        public void New_car_has_no_inertia()
        {
            var car = new Car(new Inertia(0, 0));
            Assert.Equal(new Inertia(0,0), car.Inertia);
        }

        [Theory]
        [InlineData(Adjustment.Center, 0, 0)]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.East, 1, 0)]
        [InlineData(Adjustment.NorthEast, 1, 1)]
        public void When_adjusting_in_direction_the_inertia_of_the_car_pulls_in_that_direction(Adjustment adjustment, int x, int y)
        {
            var car = new Car(new Inertia(0, 0));
            car.Adjust(adjustment);
            Assert.Equal(new Inertia(x,y), car.Inertia);
        }

        [Fact]
        public void When_adjusting_west_twice_then_the_inertia_of_the_car_pulls_west_twice()
        {
            var car = new Car(new Inertia(0, 0));
            car.Adjust(Adjustment.West);
            car.Adjust(Adjustment.West);
            Assert.Equal(new Inertia(-2,0), car.Inertia);
        }

        [Fact]
        public void When_adjusting_a_car_with_inertia_then_adjustment_is_added_to_inertia()
        {
            var car = new Car(new Inertia(-1,-1));
            car.Adjust(Adjustment.West);
            Assert.Equal(new Inertia(-2,-1), car.Inertia);
        }

        [Fact]
        public void New_car_is_at_position_zero()
        {
            var car = new Car(new Inertia(-1,-1));
            
            Assert.Equal(new Position(0, 0), car.Position);
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
        public Car(Inertia inertia)
        {
            Inertia = inertia;
            Position = new Position(0, 0);
        }

        public Inertia Inertia { get; private set; }
		public Position Position { get; internal set; }

		public void Adjust(Adjustment adjustment)
        {
            Inertia = Inertia.Add(Inertia.DirectionFor(adjustment));
        }
    }

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
