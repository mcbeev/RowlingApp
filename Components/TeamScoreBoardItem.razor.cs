using Microsoft.AspNetCore.Components;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Threading.Tasks;

namespace RowlingApp.Components
{

    public partial class TeamScoreBoardItem : ComponentBase
    {
        [Parameter]
        public string TeamClodeName { get; set; }

        [Parameter]
        public Team Team { get; set; }

        [Inject]
        private TeamService TeamService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TeamService.OnChange += StateHasChanged;

            if (Team == null)
            {
                Team = await TeamService.GetTeamByCodeNameAsync(TeamClodeName);
            }
        }
    }
}
