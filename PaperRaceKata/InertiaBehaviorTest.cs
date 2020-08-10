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
            Assert.Equal(new Inertia(0, 0), car.Inertia);
        }

        [Theory]
        [InlineData(Adjustment.Center, 0, 0)]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.East, 1, 0)]
        [InlineData(Adjustment.NorthEast, 1, 1)]
        public void When_adjusting_in_direction_the_inertia_of_the_car_pulls_in_that_direction(Adjustment adjustment, int x, int y)
        {
            var car = new Car(new Inertia(0, 0))
                .With(adjustment);
            Assert.Equal(new Inertia(x, y), car.Inertia);
        }

        [Fact]
        public void When_adjusting_west_twice_then_the_inertia_of_the_car_pulls_west_twice()
        {
            var car = new Car(new Inertia(0, 0))
                .With(Adjustment.West)
                .With(Adjustment.West);
            Assert.Equal(new Inertia(-2, 0), car.Inertia);
        }

        [Fact]
        public void When_adjusting_a_car_with_inertia_then_adjustment_is_added_to_inertia()
        {
            var car = new Car(new Inertia(-1, -1))
                .With(Adjustment.West);
            Assert.Equal(new Inertia(-2, -1), car.Inertia);
        }

        [Fact]
        public void New_car_is_at_position_zero()
        {
            var car = new Car(new Inertia(-1, -1));

            Assert.Equal(new Position(0, 0), car.Position);
        }
    }
}
