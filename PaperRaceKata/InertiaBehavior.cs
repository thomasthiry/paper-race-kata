using System.Collections.Generic;
using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class InertiaBehavior
    {
        [Fact]
        public void New_car_has_no_inertia()
        {
            var car = ACar().With(new Inertia(0, 0)).Build();
            Assert.Equal(new Inertia(0, 0), car.Inertia);
        }

        [Theory]
        [InlineData(Adjustment.Center, 0, 0)]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.East, 1, 0)]
        [InlineData(Adjustment.NorthEast, 1, 1)]
        public void When_adjusting_in_direction_the_inertia_of_the_car_pulls_in_that_direction(Adjustment adjustment, int x, int y)
        {
            var car = ACar()
                .With(new Inertia(0, 0))
                .Build();
                
            var pulledCar = car.Apply(adjustment);

            Assert.Equal(new Inertia(x, y), pulledCar.Inertia);
        }

        [Fact]
        public void When_adjusting_west_twice_then_the_inertia_of_the_car_pulls_west_twice()
        {
            var car = ACar().With(new Inertia(0, 0)).Build();

            var pulledCar = car
                .Apply(Adjustment.West)
                .Apply(Adjustment.West);
            
            Assert.Equal(new Inertia(-2, 0), pulledCar.Inertia);
        }

        [Fact]
        public void When_adjusting_a_car_with_inertia_then_adjustment_is_added_to_inertia()
        {
            var car = ACar().With(new Inertia(-1, -1)).Build();

            var pulledCar = car
                .Apply(Adjustment.West);

            Assert.Equal(new Inertia(-2, -1), pulledCar.Inertia);
        }

    }
}
