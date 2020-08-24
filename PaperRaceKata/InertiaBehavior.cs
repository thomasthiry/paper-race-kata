using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class InertiaBehavior
    {
        private static readonly Position InitialPosition = new Position(0,0);

        [Fact]
        public void New_car_has_no_inertia()
        {
            var car = ACar().Build();

            var carInNextTurn = car.Apply(Adjustment.Center);
            
            Assert.Equal(InitialPosition, carInNextTurn.Position);
        }

        [Theory]
        [InlineData(Adjustment.West, -2, 0)]
        [InlineData(Adjustment.East, 2, 0)]
        [InlineData(Adjustment.NorthEast, 2, 2)]
        public void A_car_has_inertia_in_the_next_move(Adjustment adjustment, int x, int y)
        {
            var car = ACar().Build();
                
            var carWithInertia = car.Apply(adjustment);
            var pulledCar = carWithInertia.Apply(Adjustment.Center);

            Assert.Equal(new Position(x, y), pulledCar.Position);
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
