using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RowlingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        private List<Team> Teams;
        protected string jumboDisplay { get; set; }
        protected bool IsDisabled { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await TeamService.GetAllTeamsAsync();
            IsDisabled = false;
        }

        private async void IncrementScore(string TeamSlug)
        {
            //Disable the button action to prevent button spamming
            IsDisabled = true;
            StateHasChanged();

            //Increment team score
            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamScore++;

            //Save state
            await TeamService.UpdateTeamAsync(t);

            //Re-enable the button
            IsDisabled = false;
            StateHasChanged();
        }

        private async Task IncrementFrames(string TeamSlug)
        {
            IsDisabled = true;
            StateHasChanged();

            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamFramesLeft--;
            if(t.TeamFramesLeft < 0)
            {
                t.TeamFramesLeft = 0;
            }

            await TeamService.UpdateTeamAsync(t);

            IsDisabled = false;
            StateHasChanged();
        }

        private async void ResetTeam(string TeamSlug)
        {
            IsDisabled = true;
            StateHasChanged();

            Team t = Teams.First(x => x.PageUrl == TeamSlug);
            t.TeamFramesLeft = RowlingApp.Constants.RowlingAppConstants.DefaultFramesLeft;
            t.TeamScore = 0;

            await TeamService.UpdateTeamAsync(t);

            IsDisabled = false;
            StateHasChanged();
        }

        protected void RemoveJumbo()
        {
            jumboDisplay = "none";
        }
    }
}
