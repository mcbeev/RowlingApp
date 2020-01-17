using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        private List<Team> Teams;
        protected string jumboDisplay { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        private TeamService TeamService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TeamService.OnChange += StateHasChanged;

            Teams = await TeamService.GetAllTeamsAsync();

            StartAutoRefresh();
        }

        protected void RemoveJumbo()
        {
            jumboDisplay = "none";
        }

        private void StartAutoRefresh()
        {
            /* We are passing the "TimerCallback" delegate as first parameter which points our Callback function.
             * The second parameter is null as we do not want to track any object state. 
             * We are passing 1000 as third parameter which tells the Timer to wait for one second after its creation. This third parameter == “Delay Time”. 
             * Finally, we are passing 1000 as fourth parameter which sets the regular interval for invoking the Callback function. 
             * Pass 1000 as parameter the Callback function gets called for every single second.
             * */

            var timer = new Timer(new TimerCallback(_ =>
                {
                    InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
            ), null, 1000, 1000);
        }
    }
}
