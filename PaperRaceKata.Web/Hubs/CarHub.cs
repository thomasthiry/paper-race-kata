using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PaperRaceKata.Web.Hubs
{
    public class CarHub : Hub
    {
        public async Task Adjust(string carId, string direction)
        {
            await Clients.All.SendAsync("CarAdjusted", carId, direction);
        }
    }
}
