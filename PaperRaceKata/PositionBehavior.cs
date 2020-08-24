using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class PositionBehavior
    {
        [Fact]
        public void New_car_is_at_start_position()
        {
            var car = ACar()
                .With(new Position(0,0))
                .Build();

            Assert.Equal(new Position(0, 0), car.Position);
        }

        [Fact]
        public void Car_without_inertia_and_with_center_adjustment_remains_at_the_same_location()
        {
            var expectedPosition = new Position(1, 1);
            var car = ACar()
                .With(expectedPosition)
                .Build()
                .Apply(Adjustment.Center);
             
            Assert.Equal(expectedPosition, car.Position);
        }
    }
}