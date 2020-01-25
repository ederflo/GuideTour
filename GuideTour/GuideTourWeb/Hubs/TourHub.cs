using GuideTourData.Models;
using GuideTourWeb.Models.TourViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Hubs
{
    public class TourHub : Hub
    {
        public async Task<TourViewModel> SendNewRequestedTour(TourViewModel tour)
        {
            await Clients.All.SendAsync("NewRequestedTour", tour);
            return tour;
        }
    }
}
