using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;
using RowlingApp.Models;

namespace RowlingApp.Services
{
    
    public class TeamService : ITeamService
    {
        private KontentDeliveryService _deliveryService;

        public TeamService(KontentDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public async Task<IReadOnlyList<Team>> GetAllTeamsAsync()
        {
            var data = await _deliveryService.GetDeliveryClient().GetItemsAsync<Team>();
            return data.Items;
        }

        public async Task<Team> GetTeamByNameAsync(string TeamName)
        {
            var data = await _deliveryService.GetDeliveryClient().GetItemsAsync<Team>();
            return data.Items.FirstOrDefault();
        }
    }
}
