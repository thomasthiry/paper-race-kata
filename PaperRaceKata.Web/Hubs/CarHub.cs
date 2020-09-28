using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PaperRaceKata.Web.Hubs
{
    public class CarHub : Hub
    {

        Car car = new Car(new Inertia(0,0), new Position(0,0));

        public async Task Adjust(string carId, Adjustment direction)
        {
            car.Apply(direction);

            await Clients.All.SendAsync("CarAdjusted", carId, direction, car.Position.ToString());
        }
    }
}
