using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;
using RowlingApp.Models;

namespace RowlingApp.Services
{
    public interface ITeamService
    {
        Task<IReadOnlyList<Team>> GetAllTeamsAsync();

        Task<Team> GetTeamByNameAsync(string TeamName);
    }
}
