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
        private async void DecrementScore()
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "Are you sure you want to DECREASE your SCORE?" });
            if (!confirmed) return;

            //Disable the button action to prevent button spamming
            IsDisabled = true;
            StateHasChanged();

            //Increment team score
            if(Team.TeamScore > 0){
                Team.TeamScore--;
            }

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
        private async Task DecrementFrames()
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "Are you sure you want to GO BACK a frame?" });
            if (!confirmed) return;

            IsDisabled = true;
            StateHasChanged();

            Team.TeamFramesLeft++;
            if (Team.TeamFramesLeft > RowlingApp.Constants.RowlingAppConstants.DefaultFramesLeft)
            {
                Team.TeamFramesLeft = RowlingApp.Constants.RowlingAppConstants.DefaultFramesLeft;
            }

            await TeamService.UpdateTeamAsync(Team);

            IsDisabled = false;
            StateHasChanged();
        }

        private async void ResetTeam()
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", new object[] { "RESET will set your SCORE to ZERO and will put you back to the FIRST FRAME.\n\nAre you sure your RESET? " });
            if (!confirmed) return;

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
