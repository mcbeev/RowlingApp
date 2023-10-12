using Kontent.Ai.Delivery.Abstractions;
using Kontent.Ai.Management;
using Kontent.Ai.Management.Models.LanguageVariants;
using Kontent.Ai.Management.Models.LanguageVariants.Elements;
using Kontent.Ai.Management.Models.Shared;
using Kontent.Ai.Management.Modules.Extensions;
using Kontent.Ai.Management.Modules.ModelBuilders;
using Kontent.Ai.Urls.Delivery.QueryParameters;
using Kontent.Ai.Urls.Delivery.QueryParameters.Filters;
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
        private KontentManagementService _managementService;
        // private KontentManagementBetaService _managementBetaService;
        private IDeliveryClient _deliveryClient;
        private List<Team> _allTeams;

        public event Action OnChange;

        public TeamService(KontentManagementService managementService, IDeliveryClient deliveryClient)
        {
            _managementService = managementService;
            _deliveryClient = deliveryClient;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            if (_allTeams == null)
            {
                _allTeams = new List<Team>();

                var data = await _deliveryClient.GetItemsAsync<Models.Generated.Team>(
                    new LimitParameter(3),
                    new DepthParameter(0),
                    new OrderParameter("elements.teamname")
                );
                
                _allTeams.AddRange(data.Items.Select(item => MapTeam(item)));
            }
            return _allTeams;
        }

        public async Task<Team> GetTeamByCodeNameAsync(string TeamCodeName)
        {
            var data = await _deliveryClient.GetItemAsync<Models.Generated.Team>(TeamCodeName);

            return MapTeam(data.Item);
        }

        public Team GetTeamByCodeName(string TeamCodeName)
        {
            return _allTeams.Find(t => t.CodeName == TeamCodeName);         
        }

        public async Task<bool> UpdateTeamAsync(Team TeamToUpdate)
        {
            bool success = false;

            // Get a Kontent Management API client for v1 of the API
            ManagementClient client = _managementService.GetManagementClient();

            // Create identifiers for working with our item
            var identifier = KontentManagementHelper.GetIdentifiers(TeamToUpdate.Id);

            // Specify fields we want to update in our content item
            var elements = ElementBuilder.GetElementsAsDynamic(new BaseElement[]
            {
                new NumberElement()
                {
                    Element = Reference.ByCodename(Models.Generated.Team.TeamscoreCodename),
                    Value = TeamToUpdate.TeamScore,
                },
                new NumberElement()
                {
                    Element = Reference.ByCodename(Models.Generated.Team.TeamframesleftCodename),
                    Value = TeamToUpdate.TeamFramesLeft,
                }

            });

            var upsertModel = new LanguageVariantUpsertModel() { Elements = elements };

            try
            {
                //Create a new version of the content item in Kontent   
                await client.CreateNewVersionOfLanguageVariantAsync(identifier);
                //Commit the update to Kontent
                var responseVariant = await client.UpsertLanguageVariantAsync(identifier, upsertModel);

                //Publish the version of the content item in Kontent
                await client.PublishLanguageVariantAsync(identifier);

                //Update the local memory object
                var localTeam = _allTeams.Find(t => t.CodeName == TeamToUpdate.CodeName);
                localTeam.TeamScore = TeamToUpdate.TeamScore;
                localTeam.TeamFramesLeft = TeamToUpdate.TeamFramesLeft;
            }
            catch (Exception ex)
            {
                //TODO: log the update error
                string message = ex.Message;
            }

            return success;
        }

        public void ClearLocalCache()
        {
            _allTeams = null;
        }

        private Team MapTeam(Models.Generated.Team TeamToMap)
        {
            return new Team()
            {
                TeamName = TeamToMap.Teamname,
                TeamCaptian = TeamToMap.Teamcaptian,
                TeamLogo = TeamToMap.Teamlogo.First().Url,
                TeamMembers = TeamToMap.Teammembers,
                PageUrl = TeamToMap.Pageurl,
                TeamScore = TeamToMap.Teamscore.HasValue ? (int)TeamToMap.Teamscore.Value : 0,
                TeamFramesLeft = TeamToMap.Teamframesleft.HasValue ? (int)TeamToMap.Teamframesleft.Value : RowlingAppConstants.DefaultFramesLeft,
                Id = TeamToMap.System.Id,
                CodeName = TeamToMap.System.Codename
            };
        }

        private void NotifyDataChanged() => OnChange?.Invoke();
    }

}
