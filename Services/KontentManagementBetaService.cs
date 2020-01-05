using Microsoft.Extensions.Configuration;
using RowlingApp.Constants;
using RowlingApp.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RowlingApp.Services
{
    public class KontentManagementBetaService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public KontentManagementBetaService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>("ManagementOptions:APIKey"));
        }

        public async Task<bool> CreateContentItemNewVersion(string ContentItemCodeName)
        {
            //Need to PUT to ~/{project_id}/items/{item_identifier}/variants/{language_identifier}/new-version
            HttpResponseMessage response = 
                await PutRequest(ContentItemCodeName, RowlingAppConstants.KontentManagementBetaServiceCreateNewVersionAction);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PublishContentItem(string ContentItemCodeName)
        {
            //Need to PUT to ~/{project_id}/items/{item_identifier}/variants/{language_identifier}/publish
            HttpResponseMessage response = 
                await PutRequest(ContentItemCodeName, RowlingAppConstants.KontentManagementBetaServicePublishAction);

            return response.IsSuccessStatusCode;
        }

        private async Task<HttpResponseMessage> PutRequest(string ContentItemCodeName, string MethodEndPointName)
        {
            string projectId = _configuration.GetValue<string>("ManagementOptions:ProjectId");

            var ids = KontentManagementHelper.GetIdentifiers(ContentItemCodeName);

            //Now build the PUT url
            string url = 
                $"{RowlingAppConstants.KontentManagementBetaServiceUrlBase}{projectId}/items/codename/{ids.Item1.Codename}/variants/codename/{ids.Item2.Codename}/{MethodEndPointName}";

            //Commit the request to Kontent
            return await _httpClient.PutAsync(url, null);
        }
    }
}