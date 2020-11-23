using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PaperRaceKata.Web.Hubs
{
    public class CarHub : Hub
    {

        private static Car car = new Car(new Inertia(0,0), new Position(0,0));

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("CarAdjusted", "red car", Adjustment.Center, car.Position);
        }

        public async Task JoinGame(string carId)
        {
            await Clients.All.SendAsync("CarJoined", carId, car.Position);
        }
        public async Task Adjust(string carId, Adjustment direction)
        {
            car = car.Apply(direction);

            await Clients.All.SendAsync("CarAdjusted", carId, direction, car.Position);
        }

        public async Task Reset()
        {
            car = new Car(new Inertia(0, 0), new Position(0, 0));

            await Clients.All.SendAsync("RaceReset");
        }
    }
}
