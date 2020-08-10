using Xunit;

namespace PaperRaceKata
{
    public class PositionBehavior
    {
        [Fact]
        public void New_car_is_at_position_zero()
        {
            var car = new Car(new Inertia(-1, -1));

            Assert.Equal(new Position(0, 0), car.Position);
        }
    }
}