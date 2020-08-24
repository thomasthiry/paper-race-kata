using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class InertiaBehavior
    {
        private static readonly Position InitialPosition = new Position(0,0);

        [Theory]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.Center, 0, 0)]
        public void New_car_has_no_inertia(Adjustment adjustment, int x, int y)
        {
            var car = ACar().Build();

            var carInNextTurn = car.Apply(adjustment);
            
            Assert.Equal(new Position(x,y), carInNextTurn.Position);
        }

        [Theory]
        [InlineData(Adjustment.West, -2, 0)]
        [InlineData(Adjustment.East, 2, 0)]
        [InlineData(Adjustment.NorthEast, 2, 2)]
        public void A_car_has_inertia_in_the_next_move(Adjustment adjustment, int x, int y)
        {
            var car = ACar().Build();
                
            var carWithInertia = car.Apply(adjustment);
            var carWithInertiaApplied = carWithInertia.Apply(Adjustment.Center);

            Assert.Equal(new Position(x, y), carWithInertiaApplied.Position);
        }

        [Fact]
        public void When_adjusting_west_twice_then_the_inertia_of_the_car_pulls_west_twice()
        {
            var car = ACar().Build()
                    .Apply(Adjustment.West)
                    .Apply(Adjustment.West);

            var carWithInertiaApplied = car.Apply(Adjustment.Center);
            
            Assert.Equal(new Position(-5, 0), carWithInertiaApplied.Position);
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
