using Xunit;

namespace PaperRaceKata
{
    public class PositionBehavior
    {
        [Fact]
        public void New_car_is_at_position_zero()
        {
            var car = new Car(new Inertia(-1, -1), new Position(0,0));

            Assert.Equal(new Position(0, 0), car.Position);
        }
        [Fact]
        public void Car_without_inertia_and_adjustment_center_remains_at_the_same_location()
        {
            var car = new Car(new Inertia(0, 0), new Position(1, 1))
                .With(Adjustment.Center);
             
            Assert.Equal(new Position(1, 1), car.Position);
        }
    }
}