using Microsoft.AspNetCore.Components;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RowlingApp.Components
{

    public partial class LeaderboardList : ComponentBase
    {
        [Inject]
        private TeamService TeamService { get; set; }

        private List<Team> Teams;

        protected override async Task OnInitializedAsync()
        {
            TeamService.OnChange += StateHasChanged;

            Teams = await TeamService.GetAllTeamsAsync();
        }
    }
}
