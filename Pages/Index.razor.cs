﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RowlingApp.Models;
using RowlingApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RowlingApp.Pages
{
    public partial class Index : ComponentBase
    {
        private List<Team> Teams;
        protected string jumboDisplay { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await TeamService.GetAllTeamsAsync();    
        }

        protected void RemoveJumbo()
        {
            jumboDisplay = "none";
        }
    }
}
