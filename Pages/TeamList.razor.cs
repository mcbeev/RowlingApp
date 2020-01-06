using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RowlingApp.Pages
{
    public partial class TeamList : ComponentBase
    {
        private List<Team> Teams;
        protected string jumboDisplay { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await TeamService.GetAllTeamsAsync();
            Teams = Teams.OrderBy(t => t.TeamName).ToList();
        }
    }
}
