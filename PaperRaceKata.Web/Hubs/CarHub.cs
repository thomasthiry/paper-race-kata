using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PaperRaceKata.Web.Hubs
{
    public class CarHub : Hub
    {

        private static Dictionary<string, Car> cars = new Dictionary<string, Car>();


        public async Task JoinGame(string carId)
        {
            var newCar = new Car(new Inertia(0, 0), new Position(0, 0));
            cars.Add(carId, newCar);
            await Clients.All.SendAsync("CarJoined", carId, newCar.Position);
        }
        public async Task Adjust(string carId, Adjustment direction)
        {
            cars[carId] = cars[carId].Apply(direction);

            await Clients.All.SendAsync("CarAdjusted", carId, direction, cars[carId].Position);
        }

        public async Task Reset()
        {
            cars = new Dictionary<string, Car>();

            await Clients.All.SendAsync("RaceReset");
        }
    }
}
