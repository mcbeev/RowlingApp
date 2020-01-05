using RowlingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RowlingApp.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeamsAsync();

        Task<Team> GetTeamByNameAsync(string TeamName);

        Task<bool> UpdateTeamAsync(Team TeamToUpdate);
    }
}
