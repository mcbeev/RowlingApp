using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.FeatureManagement;
using RowlingApp.Features;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        protected string jumboDisplay { get; set; }

        protected string featureDisplay { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        private TeamService TeamService { get; set; }

        [Inject]
        private IFeatureManager FeatureManager { get; set; }

        private List<Team> Teams;

        private Timer ReloadTimer;

        protected override async Task OnInitializedAsync()
        {
            TeamService.OnChange += StateHasChanged;

            Teams = await TeamService.GetAllTeamsAsync();

            if (await FeatureManager.IsEnabledAsync(nameof(FeatureFlags.LiveReload)))
            {
                StartLiveReload();
            }
            else
            {
                StopLiveReload();
            }
        }

        protected void RemoveJumbo()
        {
            jumboDisplay = "none";
        }

        private void StartLiveReload()
        {
            /* First param: the "TimerCallback" delegate which points our callback function.
             * Second param: null as we do not want to track any object state. 
             * Third param: Delay time: We are passing 1000 as third parameter which tells the Timer to wait for one second after its creation. 
             * Fourth param: we are passing 1000 which sets the regular interval for invoking the callback function. 
             * */
            ReloadTimer = new Timer(new TimerCallback(_ =>
                {
                    InvokeAsync(async () =>
                    {
                        if (await FeatureManager.IsEnabledAsync(nameof(FeatureFlags.LiveReload)))
                        {
                            System.Console.WriteLine($"State Has Changed {System.DateTime.Now}");
                            if((System.DateTime.Now.Second == 0) || (System.DateTime.Now.Second == 30))
                            {
                                TeamService.ClearLocalCache();
                                Teams = await TeamService.GetAllTeamsAsync();
                            }
                            
                            StateHasChanged();
                        }
                        else
                        {
                            //We need to stop the running timer
                            ReloadTimer.Change(Timeout.Infinite, Timeout.Infinite);
                        }
                    });
                }
            ), null, 1000, 1000);
        }

        private void StopLiveReload()
        {
            featureDisplay = "none";

            if (ReloadTimer == null)
                return;

            //We need to stop the running timer
            ReloadTimer.Change(Timeout.Infinite, Timeout.Infinite);

        }
    }
}
