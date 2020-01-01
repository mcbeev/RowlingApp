using Microsoft.AspNetCore.Components;
using RowlingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.JSInterop;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        private List<Team> Teams;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        private string jumboDisplay = "block";

        protected override async Task OnInitializedAsync()
        {
            Teams = await TeamService.GetAllTeamsAsync();
        }

        private void IncrementScore(string TeamSlug)
        {
            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamScore++;
            
            //JSRuntime.InvokeAsync<object>("rowling.updateTeamScore", TeamSlug, t.TeamScore++);
        }

        private void IncrementFrames(string TeamSlug)
        {
            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamFramesLeft--;
            if(t.TeamFramesLeft < 0)
            {
                t.TeamFramesLeft = 0;
            }

            //JSRuntime.InvokeAsync<object>("rowling.updateTeamFrames", TeamSlug, t.TeamFramesLeft);
        }

        private void ResetTeam(string TeamSlug)
        {
            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamFramesLeft = 10;
            t.TeamScore = 0;
        }

        private void RemoveJumbo()
        {
            jumboDisplay = "none";
        }
    }
}
