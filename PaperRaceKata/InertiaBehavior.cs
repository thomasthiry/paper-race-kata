using Xunit;
using static PaperRaceKata.CarBuilder;

namespace PaperRaceKata
{
    public class InertiaBehavior
    {
        [Theory]
        [InlineData(Adjustment.Center, 0, 0)]
        [InlineData(Adjustment.North, 0, 1)]
        [InlineData(Adjustment.NorthEast, 1, 1)]
        [InlineData(Adjustment.East, 1, 0)]
        [InlineData(Adjustment.SouthEast, 1, -1)]
        [InlineData(Adjustment.South, 0, -1)]
        [InlineData(Adjustment.SouthWest, -1, -1)]
        [InlineData(Adjustment.West, -1, 0)]
        [InlineData(Adjustment.NorthWest, -1, 1)]
        public void New_car_has_no_inertia(Adjustment adjustment, int x, int y)
        {
            var car = ACar().Build();

            var carWithInertiaApplied = car.Apply(adjustment);

            Assert.Equal(new Position(x, y), carWithInertiaApplied.Position);
        }

        [Theory]
        [InlineData(Adjustment.West, -2, 0)]
        [InlineData(Adjustment.East, 2, 0)]
        [InlineData(Adjustment.NorthEast, 2, 2)]
        public void A_car_has_inertia_in_the_next_move(Adjustment adjustment, int x, int y)
        {
            var car = ACar().Build()
                .Apply(adjustment);

            var carWithInertiaApplied = car.Apply(Adjustment.Center);

            Assert.Equal(new Position(x, y), carWithInertiaApplied.Position);
        }

        [Fact]
        public void A_car_builds_up_inertia_from_previous_moves()
        {
            var car = ACar().Build()
                .Apply(Adjustment.West)
                .Apply(Adjustment.West);

            var carWithInertiaApplied = car.Apply(Adjustment.Center);

            Assert.Equal(new Position(-5, 0), carWithInertiaApplied.Position);
        }
    }
}
