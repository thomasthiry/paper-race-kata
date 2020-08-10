using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class PositionBehavior
    {
        [Fact]
        public void New_car_is_at_position_zero()
        {
            var car = ACar()
                .With(new Position(0,0))
                .Build();

            Assert.Equal(new Position(0, 0), car.Position);
        }

        [Fact]
        public void Car_without_inertia_and_adjustment_center_remains_at_the_same_location()
        {
            var expectedPosition = new Position(1, 1);
            var car = ACar()
                    .With(new Inertia(0, 0))
                    .With(expectedPosition).Build()
                .With(Adjustment.Center);
             
            Assert.Equal(expectedPosition, car.Position);
        }
    }

    public class CarBuilder
    {
        private Position _position = new Position(0, 0);
        private Inertia _inertia = new Inertia(0, 0);

        public static CarBuilder ACar()
        {
            return new CarBuilder();
        }

        public CarBuilder With(Position position)
        {
            this._position = position;
            return this;
        }

        public Car Build()
        {
            return new Car(_inertia, _position);
        }

        public CarBuilder With(Inertia inertia)
        {
            _inertia = inertia;
            return this;
        }
    }
}