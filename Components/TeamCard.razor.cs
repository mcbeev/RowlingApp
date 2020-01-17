using Microsoft.AspNetCore.Components;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Threading.Tasks;

namespace RowlingApp.Components
{
    public partial class TeamCard : ComponentBase
    {
        [Parameter]
        public string TeamClodeName { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; }

        protected bool IsDisabled { get; set; }

        [Parameter]
        public Team Team { get; set; }

        [Inject]
        private TeamService TeamService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TeamService.OnChange += StateHasChanged;

            //ask memory object if we have it
            if (Team == null)
            {
                Team = TeamService.GetTeamByCodeName(TeamClodeName);
            }

            //ask Kontent if we still don't have it
            if (Team == null)
            {
                Team = await TeamService.GetTeamByCodeNameAsync(TeamClodeName);
            }

            IsDisabled = false;
        }

        private async void IncrementScore()
        {
            //Disable the button action to prevent button spamming
            IsDisabled = true;
            StateHasChanged();

            //Increment team score
            Team.TeamScore++;

            //Save state
            await TeamService.UpdateTeamAsync(Team);

            //Re-enable the button
            IsDisabled = false;
            StateHasChanged();
    }

        private async Task IncrementFrames()
        {
            IsDisabled = true;
            StateHasChanged();

            Team.TeamFramesLeft--;
            if (Team.TeamFramesLeft < 0)
            {
                Team.TeamFramesLeft = 0;
            }

            await TeamService.UpdateTeamAsync(Team);

            IsDisabled = false;
            StateHasChanged();
        }

        private async void ResetTeam()
        {
            IsDisabled = true;
            StateHasChanged();

            Team.TeamFramesLeft = RowlingApp.Constants.RowlingAppConstants.DefaultFramesLeft;
            Team.TeamScore = 0;

            await TeamService.UpdateTeamAsync(Team);

            IsDisabled = false;
            StateHasChanged();
        }
    }
}
