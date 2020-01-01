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

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            List<Team> teams = new List<Team>();

            var data = await _deliveryService.GetDeliveryClient().GetItemsAsync<RowlingApp.Models.Generated.Team>();
            
            foreach(var t in data.Items)
            {
                Team team = new Team()
                {
                    TeamName = t.TeamName,
                    TeamCaptian = t.TeamCaptian,
                    TeamLogo = t.TeamLogo.First().Url,
                    TeamMembers = t.TeamMembers,
                    PageUrl = t.PageUrl,
                    TeamScore = 0,
                    TeamFramesLeft = 10
                };
                teams.Add(team);
            }
            return teams;
        }

        public async Task<Team> GetTeamByNameAsync(string TeamName)
        {
            var data = await _deliveryService.GetDeliveryClient().GetItemsAsync<Team>();
            return data.Items.FirstOrDefault();
        }
    }
}
