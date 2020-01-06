using Kentico.Kontent.Management;
using RowlingApp.Constants;
using RowlingApp.Helpers;
using RowlingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RowlingApp.Services
{
    public class TeamService : ITeamService
    {
        private KontentDeliveryService _deliveryService;
        private KontentManagementService _managementService;
        private KontentManagementBetaService _managementBetaService;

        public TeamService(KontentDeliveryService deliveryService, KontentManagementService managementService, KontentManagementBetaService managementBetaService)
        {
            _deliveryService = deliveryService;
            _managementService = managementService;
            _managementBetaService = managementBetaService;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            List<Team> teams = new List<Team>();

            var data = await _deliveryService.GetDeliveryClient().GetItemsAsync<Models.Generated.Team>();

            foreach (var t in data.Items)
            {
                Team team = MapTeam(t);
                teams.Add(team);
            }
            return teams;
        }

        public async Task<Team> GetTeamByCodeNameAsync(string TeamCodeName)
        {
            var data = await _deliveryService.GetDeliveryClient().GetItemAsync<Models.Generated.Team>(TeamCodeName);

            return MapTeam(data.Item);
        }

        public async Task<bool> UpdateTeamAsync(Team TeamToUpdate)
        {
            bool success = false;

            //Get a Kontent Management API client for v1 of the API
            ManagementClient client = _managementService.GetManagementClient();

            //Create identifiers for working with our item
            var ids = KontentManagementHelper.GetIdentifiers(TeamToUpdate.CodeName);

            //Specify fields we want to update in our content item
            Models.Generated.TeamForScore updateModel = new Models.Generated.TeamForScore()
            {
                TeamScore = new Decimal(TeamToUpdate.TeamScore),
                TeamFramesleft = new Decimal(TeamToUpdate.TeamFramesLeft)
            };

            try
            {
                //Create a new version of the content item in Kontent   
                success = await _managementBetaService.CreateContentItemNewVersion(TeamToUpdate.CodeName);

                //Commit the update to Kontent
                await client.UpsertContentItemVariantAsync(ids.Item3, updateModel);

                //Publish the version of the content item in Kontent
                success = await _managementBetaService.PublishContentItem(TeamToUpdate.CodeName);
            }
            catch (Exception ex)
            {
                //TODO: log the update error
                string message = ex.Message;
            }

            return success;
        }

        private Team MapTeam(Models.Generated.Team TeamToMap)
        {
            return new Team()
            {
                TeamName = TeamToMap.TeamName,
                TeamCaptian = TeamToMap.TeamCaptian,
                TeamLogo = TeamToMap.TeamLogo.First().Url,
                TeamMembers = TeamToMap.TeamMembers,
                PageUrl = TeamToMap.PageUrl,
                TeamScore = TeamToMap.TeamScore.HasValue ? (int)TeamToMap.TeamScore.Value : 0,
                TeamFramesLeft = TeamToMap.TeamFramesleft.HasValue ? (int)TeamToMap.TeamFramesleft.Value : RowlingAppConstants.DefaultFramesLeft,
                Id = TeamToMap.System.Id,
                CodeName = TeamToMap.System.Codename
            };
        }
    }

}
