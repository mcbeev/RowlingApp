using Microsoft.AspNetCore.Components;
using RowlingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        private IReadOnlyList<Team> Teams;

        protected override async Task OnInitializedAsync()
        {
            Teams = await TeamService.GetAllTeamsAsync();
        }
    }
}
